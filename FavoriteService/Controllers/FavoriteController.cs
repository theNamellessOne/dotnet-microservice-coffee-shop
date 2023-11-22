using AutoMapper;
using FavoriteService.Data.Repositories;
using FavoriteService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteService.Controllers;

[Route("api/favorite/user/{userId}")]
[ApiController]
public class FavoriteController(
        IFavoriteRepository favoriteRepository,
        ICoffeeRepository coffeeRepository,
        IUserRepository userRepository,
        IMapper mapper
    )
    : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CoffeeReadDto>> GetUserFavorites(int userId)
    {
        Console.WriteLine(userId);
        var userModel = userRepository.GetUserById(userId);
        Console.WriteLine(userModel == null ? "null" : userModel.Id);
        Console.WriteLine(userModel == null ? "null" : userModel.ExternalId);
        Console.WriteLine(userModel == null ? "null" : userModel.FullName);
        if (userModel == null) return NotFound();

        return Ok(
            mapper.Map<IEnumerable<CoffeeReadDto>>(favoriteRepository.GetUserFavoriteCoffees(userId))
        );
    }

    [HttpPost("{coffeeId}")]
    public ActionResult AddCoffeeToUserFavorites(int userId, int coffeeId)
    {
        Console.WriteLine(userId);
        Console.WriteLine(coffeeId);
        var userModel = userRepository.GetUserById(userId);
        var coffeeModel = coffeeRepository.GetCoffeeById(coffeeId);

        //user or coffee with specified id does not exist
        if (userModel == null || coffeeModel == null) return NotFound();

        var favoriteModel = favoriteRepository.GetFavoriteByUserIdAndCoffeeId(userId, coffeeId);

        //coffee already in user favorites
        if (favoriteModel != null) return BadRequest();

        favoriteRepository.AddToUserFavorites(userId, coffeeModel);
        favoriteRepository.SaveChanges();
        return Ok();
    }

    [HttpDelete("{coffeeId}")]
    public ActionResult RemoveCoffeeFromUserFavorites(int userId, int coffeeId)
    {
        var userModel = userRepository.GetUserById(userId);
        var coffeeModel = coffeeRepository.GetCoffeeById(coffeeId);

        //user or coffee with specified id does not exist
        if (userModel == null || coffeeModel == null) return NotFound();

        var favoriteModel = favoriteRepository.GetFavoriteByUserIdAndCoffeeId(userId, coffeeId);

        //coffee is not in user favorites
        if (favoriteModel == null) return BadRequest();

        favoriteRepository.RemoveFromUserFavorites(favoriteModel);
        favoriteRepository.SaveChanges();
        return Ok();
    }
}