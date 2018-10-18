using AutoMapper;
using Entities;
using Library.API.Filters;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/books")]
    public class ItemsController : Controller
    {
        private IItemRepository itemRepository;
        private IItemRequestRepository itemRequestRepository;
        private UserManager<User> userManager;
        private IItemRecordRepository itemRecordRepository;

        public ItemsController(IItemRepository icontext,
            IItemRequestRepository irContext,
            IItemRecordRepository ircContext,
            UserManager<User> manager)
        {
            itemRepository = icontext;
            itemRequestRepository = irContext;
            userManager = manager;
            itemRecordRepository = ircContext;
        }


        #region GetItems

        [HttpGet("")]
        public async Task<IActionResult> GetItemsAsync()
        {
            return Json(await itemRepository.GetItemsAsync());
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetItemAsync(int itemId)
        {

            var item = await itemRepository.GetItemAsync(itemId);

            if (item == null)
            {
                return BadRequest();
            }

            return Json(item);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetItemsForUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest();
            }

            return Json(user);
        }

        #endregion

        #region PostItems


        //[Authorize(Roles = "Librarian")]
        [HttpPost("")]
        public async Task<IActionResult> NewItemAsync([FromBody] ItemForCreationDto item)
        {
            var finalItem = new Item
            {
                Owner = await userManager.FindByEmailAsync(item.OwnerEmail),
                Description = item.Description,
                CurrentHolderEmail = item.OwnerEmail,
                PhotoUrl = item.PhotoUrl,
                Title = item.Title,
                OwnerEmail = item.OwnerEmail
            };

            var user = await userManager.FindByEmailAsync(finalItem.Owner.Email);
            user.Credit += 1;

            await itemRepository.AddItemAsync(finalItem);

            if (!await itemRepository.SaveAsync())
            {
                return BadRequest("Try agian another time.");
            }
            

            return Created($"api/items/{finalItem.Id}", finalItem);
        }

        #endregion

        #region PutItems


        [HttpPut("")]
        public async Task<IActionResult> PutItemAsync([FromBody] Item model)
        {
            if (await itemRepository.ItemExistsAsync(model.Id))
            {

            }


            return Ok();
        }


        #endregion
        
        #region PostItemRequests

        [Authorize]
        [ValidateModel]
        [HttpPost("requests")]
        public async Task<IActionResult> RequestItem([FromBody] ItemRequestForCreationDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(User.Claims.First().Value);

            ItemRequest itemRequest = new ItemRequest()
            {
                ItemId = request.BookId,
                UserId = user.Id,
                RequestDate = request.RequestDate
            };




            itemRequestRepository.AddItemRequest(itemRequest);

            if (!itemRequestRepository.Save())
            {
                return BadRequest("A problem occurred while handling your request.");
            }
            itemRequest.Item = await itemRepository.GetItemAsync(itemRequest.ItemId);

            return Created($"api/books/requests/users/{itemRequest.UserId}", itemRequest);
        }

        #endregion

        #region GetBookRequests


        [HttpGet("requests/user/")]
        public async Task<IActionResult> GetItemRequestsForUser()
        {
            var user = await userManager.FindByNameAsync(User.Claims.First().Value);

            var x = itemRequestRepository.GetItemRequestsForUser(user.Id.ToString());
            
            foreach(ItemRequest b in x)
            {
                b.Item = await itemRepository.GetItemAsync(b.ItemId);
                
                //TODO: Add this user functionality.
                //b.User = await userManager.FindByEmailAsync();
            }

            return Json(x);
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetBookRequests()
        {
            var requests = itemRequestRepository.GetItemRequests();
            foreach (ItemRequest b in requests)
            {
                b.Item = await itemRepository.GetItemAsync(b.ItemId);
                b.User = await userManager.FindByIdAsync(b.UserId);
            }
            return Json(requests);
        }


        #endregion

        #region CheckOut

        [Authorize]
        [HttpPost("claimItem")]
        public async Task<IActionResult> ItemClaimed([FromBody] ItemRecordForCreationDto record)
        {
            if (record == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(record.UserName);

            ItemRecord itemRecord = new ItemRecord()
            {
                ItemId = record.ItemId,
                UserId = user.Id,
                RecordDate = record.RequestDate
            };

            if (itemRequestRepository.ItemRequestExistsForUserAndItem(user.Id, itemRecord.ItemId))
            {
                itemRequestRepository.RemoveItemRequest(user.Id, itemRecord.ItemId);
            }


            itemRecordRepository.AddItemRecord(itemRecord);

            if (!itemRecordRepository.Save())
            {
                return BadRequest("A problem occurred while handling your request.");
            }

            return Created("", itemRecord);
        }

        [Authorize]
        [HttpGet("records/users/")]
        public async Task<IActionResult> GetRecordsForUser()
        {
            var user = await userManager.FindByNameAsync(User.Claims.First().Value);

            var bookRecords = itemRecordRepository.GetItemRecordsForUser(user.Id);
            
            /*foreach(ItemRecord b in bookRecords)
            {
                b.Item = await itemRepository.GetItemAsync(b.ItemId);
                b.Owner = await userManager.FindByEmailAsync(b.Item.Owner.Email);
                b.User = user;
            }*/

            return Json(bookRecords);
        }

        [HttpGet("records")]
        public async Task<IActionResult> GetAllBookRecords()
        {
            var bookRecords = itemRecordRepository.GetItemRecords();
            foreach (ItemRecord b in bookRecords)
            {
                b.Item = await itemRepository.GetItemAsync(b.ItemId);
                b.User = await userManager.FindByIdAsync(b.UserId);
            }
            return Ok(bookRecords);
        }

        #endregion

        #region Checkin

        [HttpDelete("records")]
        public IActionResult DeleteRecord([FromBody] ItemRecordForDeletionDto itemRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            itemRecordRepository.DeleteItemRecord(itemRecord);

            if (itemRecordRepository.Save())
            {
                return NoContent();
            }

            return BadRequest();
        }

        #endregion
    }
}
