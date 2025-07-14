using Foodtek_Application_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Foodtek_Application_API.DTOs.Task;
using Foodtek_Application_API.DTOs.Task.Item;

namespace Testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly FoodtekDbContext context;

        public TaskController(FoodtekDbContext _context)
        {
            context = _context;
        }

        //Task 7
        //1-Get All Category API : Return each activated category with
        //Id and Name and Image / Logo URL 

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {

            try
            {
                var categories = await context.Categories.Where(c => c.IsActive == true)
                .Select(c => new GetAllCategoriesDTO
                {
                    Id = c.Id,
                    NameEn = c.NameEn,
                    NameAr = c.NameAr,
                    Image = c.Image

                }).ToListAsync();

                if (!categories.Any())
                {
                    return NotFound(new
                    {
                        message = "No activated categories found."
                    });
                }

                return Ok(new
                {
                    message = "Categories retrieved successfully",
                    data = categories
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving categories",
                    details = ex.Message
                });
            }
        }
        // 2- Get All offer/Discount : Return each activated discount
        // with Id and Title and Description and Image URL if exist 

        [HttpGet("GetAllOffers")]
        public async Task<IActionResult> GetAllOffers()
        {
            try
            {
                var offers = await context.DiscountOffers.Where(d => d.IsActive == true)
                    .Select(d => new GetAllOffersDTO
                    {
                        Id = d.Id,
                        TitleEn = d.TitleEn,
                        TitleAr = d.TitleAr,
                        DescriptionEn = d.DescriptionEn,
                        DescriptionAr = d.DescriptionAr,
                        Image = d.Image

                    }).ToListAsync();

                if (!offers.Any())
                {
                    return NotFound(new
                    {
                        message = "No activated offers found."
                    });
                }

                return Ok(new
                {
                    message = "Offers retrieved successfully",
                    data = offers
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving offers",
                    details = ex.Message
                });
            }
        }


        //3- Get Top Rated Item : Return The 10 top rated element in the system
        //with (Id , Name , Description , Price , Image , Rate)
        // Items should be ordered by rate in ascending order

        [HttpGet("GetTopRatedItems")]
        public async Task<IActionResult> GetTopRatedItems()
        {
            try
            {
                var topRatedItems = await context.Items.Where(i => i.Reviews.Any())
                    .Select(i => new GetTopRatedItemsDTO
                    {
                        Id = i.Id,
                        NameEn = i.NameEn,
                        NameAr = i.NameAr,
                        DescriptionEn = i.DescriptionEn,
                        DescriptionAr = i.DescriptionAr,
                        Price = i.Price,
                        Image = i.Image,
                        Rate = i.Reviews.Average(r => r.Rate ?? 0)
                    })
                    .OrderByDescending(i => i.Rate)
                    .Take(10)
                    .ToListAsync();

                if (!topRatedItems.Any())
                {
                    return NotFound(new
                    {
                        message = "No rated items found."
                    });
                }

                return Ok(new
                {
                    message = "Top rated items retrieved successfully",
                    data = topRatedItems
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving top rated items",
                    details = ex.Message
                });
            }
        }


        //4- Get Top Recommended Item :  Return The 10 top Reordered Item in the system
        // with (Id , Name , Description , Price , Image )

        [HttpGet("GetTopRecommendedItems")]
        public IActionResult GetTopRecommendedItems()
        {
            try
            {
                var topItems = context.Carts.Where(c => c.Item != null)
                    .GroupBy(c => new
                    {
                        c.ItemId,
                        c.Item.NameEn,
                        c.Item.NameAr,
                        c.Item.DescriptionEn,
                        c.Item.DescriptionAr,
                        c.Item.Price,
                        c.Item.Image

                    }).Select(g => new GetTopRecommendedItemsDTO
                    {
                        Id = (int)g.Key.ItemId,
                        NameEn = g.Key.NameEn,
                        NameAr = g.Key.NameAr,
                        DescriptionEn = g.Key.DescriptionEn,
                        DescriptionAr = g.Key.DescriptionAr,
                        Price = g.Key.Price,
                        Image = g.Key.Image,
                        ReorderCount = g.Count()
                    })

                    .OrderByDescending(x => x.ReorderCount)
                    .Take(10)
                    .ToList();

                if (!topItems.Any())
                {
                    return NotFound(new
                    {
                        message = "No recommended items found."
                    });
                }

                return Ok(new
                {
                    message = "Top recommended items retrieved successfully",
                    data = topItems
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving top recommended items",
                    details = ex.Message
                });
            }
        }


        //5- Get Item By Category Id : Return All Item in Specific Category with
        //(Id , Name , Description , Price , Image )

        [HttpGet("GetItemsByCategory/{categoryId}")]
        public async Task<IActionResult> GetItemsByCategory([FromRoute] int categoryId)
        {
            try
            {
                var items = await context.Items.Where(i => i.CategoryId == categoryId)
                    .Select(i => new GetItemsByCategoryDTO
                    {
                        Id = i.Id,
                        NameEn = i.NameEn,
                        NameAr = i.NameAr,
                        DescriptionEn = i.DescriptionEn,
                        DescriptionAr = i.DescriptionAr,
                        Price = i.Price,
                        Image = i.Image

                    }).ToListAsync();

                if (!items.Any())
                {
                    return NotFound(new
                    {
                        message = "No items found for the given category."
                    });
                }

                return Ok(new
                {
                    message = "Items retrieved successfully",
                    data = items
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving items by category",
                    details = ex.Message
                });
            }
        }

        //6- Get Favorite Item By User ID : Return Each 
        // item in user favorite list by(Id , Name , Description , Price , Creation Date)

        [HttpGet("GetFavorite")]
        public async Task<IActionResult> GetFavorite(int userid)
        {
            try
            {
                var favorite = await context.Favorites.Where(f => f.UsersId == userid && f.Item != null)
                    .Select(f => new GetFavoriteDTO
                    {
                        Id = f.Item.Id,
                        NameEn = f.Item.NameEn,
                        NameAr = f.Item.NameAr,
                        DescriptionEn = f.Item.DescriptionEn,
                        DescriptionAr = f.Item.DescriptionAr,
                        Price = (double)f.Item.Price,
                        CreationDate = f.CreationDate ?? DateTime.MinValue

                    }).ToListAsync();

                if (!favorite.Any())
                {
                    return NotFound(new
                    {
                        message = "No favorites found for this user."
                    });
                }

                return Ok(new
                {
                    message = "Favorites retrieved successfully",
                    data = favorite
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving favorites",
                    details = ex.Message
                });
            }
        }

        //7- Get All Notification By User Id : Return All Notification List by user 
        // id with(Id , Title , Content , Date , Is Read)

        [HttpGet("GetNotification")]
        public async Task<IActionResult> GetNotification(int userid)
        {
            try
            {
                var notification = await context.Notifications.Where(n => n.UsersId == userid)
                    .Select(n => new GetNotificationDTO
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Content = n.Content,
                        CreationDate = n.CreationDate ?? DateTime.MinValue,
                        IsRead = n.IsRead

                    }).ToListAsync();

                if (!notification.Any())
                {
                    return NotFound(new
                    {
                        message = "No notifications found for this user."
                    });
                }

                return Ok(new
                {
                    message = "Notifications retrieved successfully",
                    data = notification
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving notifications",
                    details = ex.Message
                });
            }
        }

        //8- Get Item Details By Id : Get All Item Details By Given Id Such as
        //   (Id, Name, Description, Price, Rate, Number of Review )

        [HttpGet("GetItemDetails/{itemid}")]
        public async Task<IActionResult> GetItemDetails([FromRoute] int itemid)
        {
            try
            {
                var item = await context.Items.Where(i => i.Id == itemid)
                    .Select(i => new GetItemDetailsDTO
                    {
                        Id = i.Id,
                        NameEn = i.NameEn,
                        NameAr = i.NameAr,
                        DescriptionEn = i.DescriptionEn,
                        DescriptionAr = i.DescriptionAr,
                        Price = (double)i.Price,
                        Rate = i.Reviews.Count() > 0
                        ? (int)i.Reviews.Average(r => r.Rate) : 0,
                        NumberOfReview = i.Reviews.Count()

                    }).FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound(new
                    {
                        message = "Item not found."
                    });
                }

                return Ok(new
                {
                    message = "Item details retrieved successfully",
                    data = item
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving item details",
                    details = ex.Message
                });
            }
        }

        //9- Add Item To Cart by Item and User Id : Put Item In Shopping Cart
        // by send user Id and CartID and Item Selected Quantity


        [HttpPost("AddItemToCart")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddItemToCartDTO input)
        {
            try
            {
                if (input.Quantity <= 0)
                    return BadRequest("Quantity must be greater than 0.");

                // Check if item and user exist
                var itemExists = await context.Items.AnyAsync(i => i.Id == input.ItemId);
                var userExists = await context.Users.AnyAsync(u => u.Id == input.UserId);

                if (!itemExists || !userExists)
                    return NotFound("Item or User not found.");

                // Check if item already exists in user's cart
                var existingCartItem = await context.Carts
                    .FirstOrDefaultAsync(c => c.UsersId == input.UserId && c.ItemId == input.ItemId);

                if (existingCartItem != null)
                {
                    // Item exists, update the quantity and timestamp
                    existingCartItem.Quantity += input.Quantity;
                    existingCartItem.UpdateDate = DateTime.Now;
                    existingCartItem.UpdatedBy = "System";

                    context.Carts.Update(existingCartItem);
                    await context.SaveChangesAsync();

                    return Ok(new { message = "Item quantity updated in cart.", ItemId = existingCartItem.Id });
                }

                // If not found in cart, create a new entry
                var cartItem = new Cart
                {
                    ItemId = input.ItemId,
                    UsersId = input.UserId,
                    Quantity = input.Quantity,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                };

                context.Carts.Add(cartItem);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Item added to cart successfully.",
                    data = new { ItemId = cartItem.Id }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error adding item to cart",
                    details = ex.Message
                });
            }
        }

        

        //10- Add Item From Favorite list
        [HttpPost("AddItemToFavorite")]
        public async Task<IActionResult> AddItemToFavorite([FromBody] AddItemToFavoriteDTO input)
        {
            try
            {
                var item = await context.Items.AnyAsync(i => i.Id == input.Itemid);

                if (!item)
                {
                    return NotFound("Item not found.");
                }

                var favorite = await context.Favorites
                    .FirstOrDefaultAsync(f => f.UsersId == input.Userid && f.ItemId == input.Itemid);

                if (favorite != null)
                {
                    return NotFound("Item is already in your favorite list.");
                }
                var favoriteItem = new Favorite
                {
                    ItemId = input.Itemid,
                    UsersId = input.Userid,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                };

                context.Favorites.Add(favoriteItem);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Item added to your favorite list.",
                    data = new { ItemId = favoriteItem.Id }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error adding item to favorite list",
                    details = ex.Message
                });
            }

        }

        //10- Remove Item From Favorite list
        [HttpDelete("RemoveItem")]
        public async Task<IActionResult> RemoveItem(int id)
        {

            try
            {
                var item = await context.Favorites.Where(x => x.Id == id).SingleOrDefaultAsync();

                if (item != null)
                {
                    context.Favorites.Remove(item);

                    await context.SaveChangesAsync();

                    return Ok(new
                    {
                        message = "Item removed from the favorite list."
                    });
                }

                return NotFound(new
                {
                    message = "No item found with the selected ID."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while removing the item.",
                    details = ex.Message
                });
            }
        }


        //Task 10
        //1- Get Current Cart by User Id : Return All Selected Item in
        //Current Activate Cart by(Id , Item Name, Description , Quantity)
        [HttpGet("GetCurrentCart/{userid}")]
        public async Task<IActionResult> GetCurrentCart([FromRoute] int userid)
        {
            try
            {
                var JoinQuery = await (from c in context.Carts
                                       join i in context.Items
                                       on c.ItemId equals i.Id
                                       where c.UsersId == userid && c.IsActive == true
                                       select new GetCurrentCartDTO
                                       {
                                           Id = c.Id,
                                           NameEn = i.NameEn,
                                           NameAr = i.NameAr,
                                           DescriptionEn = i.DescriptionEn,
                                           DescriptionAr = i.DescriptionAr,
                                           Quantity = (int)c.Quantity

                                       }).ToListAsync();

                if (!JoinQuery.Any())

                    return NotFound(new { message = "No active cart or items found for the user." });

                return Ok(new
                {
                    message = "Active cart items retrieved successfully",
                    data = JoinQuery
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the cart items",
                    details = ex.Message
                });
            }
        }


        //2- Manage Cart Item : API To Handle add / remove / remove one paces of cart item
        //   by item id and user Id / item id and cart id  

        [HttpDelete("RemoveByUser")]
        public async Task<IActionResult> RemoveByUser([FromQuery] int userid, [FromQuery] int itemid)
        {
            try
            {
                var query = await context.Carts.FirstOrDefaultAsync(c => c.UsersId == userid && c.ItemId == itemid);

                if (query == null)
                    return NotFound("The item was not found in the user's cart.");

                context.Carts.Remove(query);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Item removed from cart successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while removing the item from the cart.",
                    details = ex.Message
                });
            }
        }


        //2- Manage Cart Item : API To Handle add / remove / remove one paces of cart item
        //   by item id and user Id / item id and cart id  

        [HttpDelete("RemoveByCart")]
        public async Task<IActionResult> RemoveByCart([FromQuery] int cartid, [FromQuery] int itemid)
        {
            try
            {
                var query = await context.Carts.FirstOrDefaultAsync(c => c.Id == cartid && c.ItemId == itemid);

                if (query == null)
                    return NotFound("The item was not found in the specified cart.");

                context.Carts.Remove(query);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Item removed from cart successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while removing the item from the cart.",
                    details = ex.Message
                });
            }
        }


        //  3- Get Order History By User Id : Return all Previous Order 
        //By(Id , Address , Date , Total Price)

        [HttpGet("GetOrderHistory/{userid}")]
        public async Task<IActionResult> GetOrderHistory([FromRoute] int userid)
        {
            try
            {
                var order = await context.Orders.Where(o => o.UsersId == userid)
                    .Select(o => new GetOrderHistoryDTO
                    {
                        Id = o.Id,
                        TotalPrice = o.TotalPrice,
                        AddressId = (int)o.AddressId,
                        CreationDate = o.CreationDate ?? DateTime.MinValue


                    }).ToListAsync();


                if (!order.Any())
                {
                    return NotFound(new
                    {
                        message = "No orders found for this user."
                    });
                }

                return Ok(new
                {
                    message = "Order history retrieved successfully",
                    data = order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error retrieving order history",
                    details = ex.Message
                });
            }
        }


        //4- Add Payment Card 

        [HttpPost("AddPayment")]
        public async Task<IActionResult> AddPayment([FromBody] AddPaymentDTO input)
        {
            try
            {
                var payment = await context.PaymentMethods
                    .FirstOrDefaultAsync(p => p.Name.ToLower() == input.Name.ToLower() && p.PaymentType == input.PaymentType);

                if (payment != null)
                {
                    return BadRequest("Payment method already exists.");
                }

                var newPayment = new PaymentMethod
                {
                    Name = input.Name,
                    PaymentType = input.PaymentType,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                };
                context.PaymentMethods.Add(newPayment);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Payment card added successfully.",
                    data = new { paymentMethodId = newPayment.Id }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error adding payment method",
                    details = ex.Message
                });
            }
        }

        //5- Add New Address / Delivery Location

        [HttpPost("AddAddress")]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressDTO input)
        {
            try
            {
                var add = await context.Addresses
                    .FirstOrDefaultAsync(a => a.Title == input.Title && a.Province == input.Province);

                if (add != null)
                {
                    return BadRequest("Address with the same title and province already exists.");
                }

                var newaddress = new Address
                {
                    Title = input.Title,
                    BuildingName = input.BuildingName,
                    BuildingNumber = input.BuildingNumber,
                    Floor = input.Floor,
                    ApartmentNumber = input.ApartmentNumber,
                    AdditionalDetails = input.AdditionalDetails,
                    Latitude = input.Latitude,
                    Longitude = input.Longitude,
                    Province = input.Province,
                    Region = input.Region,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    UsersId = input.UsersId
                };
                context.Add(newaddress);
                await context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Address added successfully.",
                    data = new { addressId = newaddress.Id }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error adding address",
                    details = ex.Message
                });
            }
        }
    }

}