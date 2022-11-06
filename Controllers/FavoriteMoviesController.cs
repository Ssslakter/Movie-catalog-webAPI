﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MovieCatalogAPI.Exceptions;
using MovieCatalogAPI.Models;
using MovieCatalogAPI.Services;

namespace MovieCatalogAPI.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoriteMoviesController : ControllerBase
    {
        private readonly IFavoriteMoviesService _favoriteMoviesService;
        private readonly ITokenCacheService _tokenCacheService;

        public FavoriteMoviesController(IFavoriteMoviesService favoriteMoviesService, ITokenCacheService tokenCacheService)
        {
            _tokenCacheService = tokenCacheService;
            _favoriteMoviesService = favoriteMoviesService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                if (await _tokenCacheService.IsTokenInDB(Request.Headers[HeaderNames.Authorization]))
                {
                    return Unauthorized("Token is expired");
                }
            }
            catch { }
            try
            {
                var movieList = await _favoriteMoviesService.GetMovies(User.Identity.Name);
                return Ok(movieList);
            }
            catch (NotFoundException)
            {
                return NotFound($"User {User.Identity.Name} was not found");
            }
            catch
            {
                return Problem(statusCode: 500, title: "Something went wrong");
            }
        }
        [HttpPost("{id}/add"), Authorize]
        public async Task<IActionResult> AddMovie([FromRoute] Guid id)
        {
            try
            {
                if (await _tokenCacheService.IsTokenInDB(Request.Headers[HeaderNames.Authorization]))
                {
                    return Unauthorized("Token is expired");
                }
            }
            catch { }
            if (!await _favoriteMoviesService.IfMovieExists(id))
            {
                return NotFound($"Movie with id {id} does not exist");
            }
            if (await _favoriteMoviesService.IfUserHasMovie(User.Identity.Name, id))
            {
                return BadRequest("User already has this movie in favorite");
            }
            await _favoriteMoviesService.AddMovie(User.Identity.Name, id);
            return Ok();
        }

        [HttpDelete("{id}/delete"), Authorize]
        public async Task<IActionResult> DeleteMovie([FromRoute] Guid id)
        {
            try
            {
                if (await _tokenCacheService.IsTokenInDB(Request.Headers[HeaderNames.Authorization]))
                {
                    return Unauthorized("Token is expired");
                }
            }
            catch { }
            if (!await _favoriteMoviesService.IfMovieExists(id))
            {
                return NotFound($"Movie with id {id} does not exist");
            }
            if (!await _favoriteMoviesService.IfUserHasMovie(User.Identity.Name, id))
            {
                return BadRequest("User does not have this movie in favorites");
            }
            await _favoriteMoviesService.RemoveMovie(User.Identity.Name, id);
            return Ok();
        }
    }
}
