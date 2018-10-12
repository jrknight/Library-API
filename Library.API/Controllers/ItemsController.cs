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
    [Route("api/items")]
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

        #endregion

        #region PostItems


        //[Authorize(Roles = "Librarian")]
        [HttpPost("")]
        public async Task<IActionResult> NewItemAsync([FromBody] ItemForCreationDto item)
        {
            if (await itemRepository.ItemExistsAsync(item.Title))
            {
                return BadRequest("Item already exists");
            }

            if (item.Title == item.Description)
            {
                ModelState.AddModelError("Summary", "The title and summary must be separate values.");
            }

            var finalItem = Mapper.Map<Item>(item);

            await itemRepository.AddItemAsync(finalItem);

            if (!await itemRepository.SaveAsync())
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }
            

            return Created($"api/items/{finalItem.Id}", finalItem);
        }

        #endregion

        #region PutBooks


        [HttpPut("")]
        public async Task<IActionResult> PutBookAsync([FromBody] Item model)
        {
            if (await itemRepository.ItemExistsAsync(model.Id))
            {

            }


            return Ok();
        }


        #endregion
        
        #region PostBookRequests

        [Authorize]
        [ValidateModel]
        [HttpPost("requests")]
        public async Task<IActionResult> RequestNewBook([FromBody] ItemRequestForCreationDto request)
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

            ItemRequest bookRequest = new ItemRequest()
            {
                ItemId = request.BookId,
                UserId = user.Id,
                RequestDate = request.RequestDate
            };


            itemRequestRepository.AddItemRequest(bookRequest);

            if (!itemRequestRepository.Save())
            {
                return BadRequest("A problem occurred while handling your request.");
            }
            bookRequest.Item = await itemRepository.GetItemAsync(bookRequest.ItemId);

            return Created($"api/books/requests/users/{bookRequest.UserId}", bookRequest);
        }

        #endregion

        #region GetBookRequests


        [HttpGet("requests/user/")]
        public async Task<IActionResult> GetBookRequestsForUser()
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
                b.Owner = await userManager.FindByEmailAsync(b.Owner.Email);
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
            
            foreach(ItemRecord b in bookRecords)
            {
                b.Item = await itemRepository.GetItemAsync(b.ItemId);
                b.Owner = await userManager.FindByEmailAsync(b.Item.OwnerEmail);
                b.User = user;
            }

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
