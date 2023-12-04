using System.Transactions;
using ebay.Constants;
using ebay.Data;
using ebay.Entity;
using ebay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Areas.Admin.Controllers;
[AllowAnonymous]
[Area("Admin")]

public class SeedingController : Controller
{
	private readonly ApplicationDbContext _context;

	public SeedingController(ApplicationDbContext context)
	{
		_context = context;
	}
	public async Task<IActionResult> SeedAdmin()
	{
		try
		{
			var adminExit = await _context.Users.AnyAsync(x => x.UserType == UserTypeConstants.Admin);
			if (!adminExit)
			{
				using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
				var admin = new User()
				{
					Email = "admin@admin.com",
					PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
					UserType = UserTypeConstants.Admin,
					FirstName = "admin",
					LastName = "admin"
				};
				await _context.AddAsync(admin);
				await _context.SaveChangesAsync();
				tx.Complete();
				return Content("Added admin!!!");
			}
			return Content("User exist");
		}
		catch (Exception e)
		{
			return Content(e.Message);
		}
	}
	public async Task<IActionResult> SeedCountriesAsync()
	{
		try
		{
			var CountryExit = await _context.Countries.AnyAsync();
			if (!CountryExit)
			{
				using (var Tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					List<Country> Countries = new List<Country>()
				{
					new() {Name="Afghanistan"},
					new(){ Name="Albania"},
					new(){ Name="Algeria"},
					new(){ Name="Andorra"},
					new(){ Name="Angola"},
					new(){ Name="Antigua & Deps"},
					new(){ Name="Argentina"},
					new(){ Name="Armenia"},
					new(){ Name="Australia"},
					new(){ Name="Austria"},
					new(){ Name="Azerbaijan"},
					new(){ Name="Bahamas"},
					new(){ Name="Bahrain"},
					new(){ Name="Bangladesh"},
					new(){ Name="Barbados"},
					new(){ Name="Belarus"},
					new(){ Name="Belgium"},
					new(){ Name="Belize"},
					new(){ Name="Benin"},
					new(){ Name="Bhutan"},
					new(){ Name="Bolivia"},
					new(){ Name="Bosnia Herzegovina"},
					new(){ Name="Botswana"},
					new(){ Name="Brazil"},
					new(){ Name="Brunei"},
					new(){ Name="Bulgaria"},
					new(){ Name="Burkina"},
					new(){ Name="Burundi"},
					new(){ Name="Cambodia"},
					new(){ Name="Cameroon"},
					new(){ Name="Canada"},
					new(){ Name="Cape Verde"},
					new(){ Name="Central African Rep"},
					new(){ Name="Chad"},
					new(){ Name="Chile"},
					new(){ Name="China"},
					new(){ Name="Colombia"},
					new(){ Name="Comoros"},
					new(){ Name="Congo"},
					new(){ Name="Congo {Democratic Rep}"},
					new(){ Name="Costa Rica"},
					new(){ Name="Croatia"},
					new(){ Name="Cuba"},
					new(){ Name="Cyprus"},
					new(){ Name="Czech Republic"},
					new(){ Name="Denmark"},
					new(){ Name="Djibouti"},
					new(){ Name="Dominica"},
					new(){ Name="Dominican Republic"},
					new(){ Name="East Timor"},
					new(){ Name="Ecuador"},
					new(){ Name="Egypt"},
					new(){ Name="El Salvador"},
					new(){ Name="Equatorial Guinea"},
					new(){ Name="Eritrea"},
					new(){ Name="Estonia"},
					new(){ Name="Ethiopia"},
					new(){ Name="Fiji"},
					new(){ Name="Finland"},
					new(){ Name="France"},
					new(){ Name="Gabon"},
					new(){ Name="Gambia"},
					new(){ Name="Georgia"},
					new(){ Name="Germany"},
					new(){ Name="Ghana"},
					new(){ Name="Greece"},
					new(){ Name="Grenada"},
					new(){ Name="Guatemala"},
					new(){ Name="Guinea"},
					new(){ Name="Guinea-Bissau"},
					new(){ Name="Guyana"},
					new(){ Name="Haiti"},
					new(){ Name="Honduras"},
					new(){ Name="Hungary"},
					new(){ Name="Iceland"},
					new(){ Name="India"},
					new(){ Name="Indonesia"},
					new(){ Name="Iran"},
					new(){ Name="Iraq"},
					new(){ Name="Ireland {Republic}"},
					new(){ Name="Israel"},
					new(){ Name="Italy"},
					new(){ Name="Ivory Coast"},
					new(){ Name="Jamaica"},
					new(){ Name="Japan"},
					new(){ Name="Jordan"},
					new(){ Name="Kazakhstan"},
					new(){ Name="Kenya"},
					new(){ Name="Kiribati"},
					new(){ Name="Korea North"},
					new(){ Name="Korea South"},
					new(){ Name="Kosovo"},
					new(){ Name="Kuwait"},
					new(){ Name="Kyrgyzstan"},
					new(){ Name="Laos"},
					new(){ Name="Latvia"},
					new(){ Name="Lebanon"},
					new(){ Name="Lesotho"},
					new(){ Name="Liberia"},
					new(){ Name="Libya"},
					new(){ Name="Liechtenstein"},
					new(){ Name="Lithuania"},
					new(){ Name="Luxembourg"},
					new(){ Name="Macedonia"},
					new(){ Name="Madagascar"},
					new(){ Name="Malawi"},
					new(){ Name="Malaysia"},
					new(){ Name="Maldives"},
					new(){ Name="Mali"},
					new(){ Name="Malta"},
					new(){ Name="Marshall Islands"},
					new(){ Name="Mauritania"},
					new(){ Name="Mauritius"},
					new(){ Name="Mexico"},
					new(){ Name="Micronesia"},
					new(){ Name="Moldova"},
					new(){ Name="Monaco"},
					new(){ Name="Mongolia"},
					new(){ Name="Montenegro"},
					new(){ Name="Morocco"},
					new(){ Name="Mozambique"},
					new(){ Name="Myanmar, {Burma}"},
					new(){ Name="Namibia"},
					new(){ Name="Nauru"},
					new(){ Name="Nepal"},
					new(){ Name="Netherlands"},
					new(){ Name="New Zealand"},
					new(){ Name="Nicaragua"},
					new(){ Name="Niger"},
					new(){ Name="Nigeria"},
					new(){ Name="Norway"},
					new(){ Name="Oman"},
					new(){ Name="Pakistan"},
					new(){ Name="Palau"},
					new(){ Name="Panama"},
					new(){ Name="Papua New Guinea"},
					new(){ Name="Paraguay"},
					new(){ Name="Peru"},
					new(){ Name="Philippines"},
					new(){ Name="Poland"},
					new(){ Name="Portugal"},
					new(){ Name="Qatar"},
					new(){ Name="Romania"},
					new(){ Name="Russian Federation"},
					new(){ Name="Rwanda"},
					new(){ Name="St Kitts & Nevis"},
					new(){ Name="St Lucia"},
					new(){ Name="Saint Vincent & the Grenadines"},
					new(){ Name="Samoa"},
					new(){ Name="San Marino"},
					new(){ Name="Sao Tome & Principe"},
					new(){ Name="Saudi Arabia"},
					new(){ Name="Senegal"},
					new(){ Name="Serbia"},
					new(){ Name="Seychelles"},
					new(){ Name="Sierra Leone"},
					new(){ Name="Singapore"},
					new(){ Name="Slovakia"},
					new(){ Name="Slovenia"},
					new(){ Name="Solomon Islands"},
					new(){ Name="Somalia"},
					new(){ Name="South Africa"},
					new(){ Name="South Sudan"},
					new(){ Name="Spain"},
					new(){ Name="Sri Lanka"},
					new(){ Name="Sudan"},
					new(){ Name="Suriname"},
					new(){ Name="Swaziland"},
					new(){ Name="Sweden"},
					new(){ Name="Switzerland"},
					new(){ Name="Syria"},
					new(){ Name="Taiwan"},
					new(){ Name="Tajikistan"},
					new(){ Name="Tanzania"},
					new(){ Name="Thailand"},
					new(){ Name="Togo"},
					new(){ Name="Tonga"},
					new(){ Name="Trinidad & Tobago"},
					new(){ Name="Tunisia"},
					new(){ Name="Turkey"},
					new(){ Name="Turkmenistan"},
					new(){ Name="Tuvalu"},
					new(){ Name="Uganda"},
					new(){ Name="Ukraine"},
					new(){ Name="United Arab Emirates"},
					new(){ Name="United Kingdom"},
					new(){ Name="United States"},
					new(){ Name="Uruguay"},
					new(){ Name="Uzbekistan"},
					new(){ Name="Vanuatu"},
					new(){ Name="Vatican City"},
					new(){ Name="Venezuela"},
					new(){ Name="Vietnam"},
					new(){ Name="Yemen"},
					new(){ Name="Zambia"},
					new(){ Name="Zimbabwe"}
				};
					_context.Countries.AddRange(Countries);
					_context.SaveChanges();
					Tx.Complete();
					return Content("countries added!!!");
				}

			}
			return Content("Countries exist");
		}
		catch (Exception e)
		{
			return Content(e.Message);
		}

	}
	public async Task<IActionResult> SeedCategoryAsync()
	{
		try
		{
			var CategoryExit = await _context.Categories.AnyAsync();
			if (!CategoryExit)
			{
				using (var Tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
				{
					var Categories = new List<Category>
					{
						new() {Name = "Electronic"},
						new() {Name = "Sports"},
						new() {Name = "Phones"},
						new() {Name = "Books"},
						new() {Name = "Arts & Crafts"},
						new() {Name = "Home and Kitchen"},
						new() {Name = "Pet Supplies"},
						new() {Name = "Software"},
						new() {Name = "Video Games"},
						new() {Name = "Health"}
					};
					_context.Categories.AddRange(Categories);
					_context.SaveChanges();
					Tx.Complete();
					return Content("Categories added!!!");
				}
			}
			return Content("Categories exist!!!");
		}
		catch (Exception e)
		{
			return Content(e.Message);
		}
	}
	public async Task<IActionResult> SeedProduct()
	{
		try
		{
			// using (var Tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
			// {
			// 	var Product = new List<Product>
			// 	{
			// 		new Product(){Name= "Apple iPhone 11, 64GB, Black - Unlocked (Renewed)",Description="This phone is unlocked and compatible with any carrier of choice on GSM and CDMA networks (e.g. AT&T, T-Mobile, Sprint, Verizon, US Cellular, Cricket, Metro, Tracfone, Mint Mobile, etc.).Tested for battery health and guaranteed to have a minimum battery capacity of 80%.Successfully passed a full diagnostic test which ensures like-new functionality and removal of any prior-user personal information.The device does not come with headphones or a SIM card. It does include a generic (Mfi certified) charger and charging cable.Inspected and guaranteed to have minimal cosmetic damage, which is not noticeable when the device is held at arm's length.",Price=267,Stock=100,Brand="Apple",CategoryId=3},
			// 		new Product{Name = "MageGee 75% Mechanical Gaming Keyboard with Red Switch, LED Blue Backlit Keyboard, 87 Keys Compact TKL Wired Computer Keyboard for Windows Laptop PC Gamer - Black/Grey",Description="[Mechanical red switch]: characterized for being linear and smoother, slight key sound has no paragraph sense with minimal resistance, but fast action without a tactile bump feel which makes it easier to tap the keyboard.[Classic charming blue LED backlight]: customize multiple illuminated light effects with static and dynamic, supports about 20 kinds backlight modes, press Fn+| to switch, FN+↑/↓ control backlight brightness, FN+←/→ control backlight speed.",Stock=100,Price=29,CategoryId=2,Brand="MageGee"}
			// 	};
			// 	await _context.Products.AddRangeAsync(Product);
			// 	_context.SaveChanges();
			// 	Tx.Complete();
			// }

		}
		catch(Exception e)
		{
			return Content(e.Message);
		}
		return Content("Product added!!!");
	}


	internal class UserManager
	{
		internal static void AddToRole(bool adminExit, string v)
		{
			throw new NotImplementedException();
		}

		internal static void AddToRole(int id, string v)
		{
			throw new NotImplementedException();
		}
	}
}