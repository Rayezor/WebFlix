using Microsoft.AspNetCore.Mvc;
using WebFlix.Models;
using System.Net;
using System.Linq;
using System.Net.Http;
using static WebFlix.Models.Movie;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebFlix.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MovieController : ControllerBase
	{
		static List<Movie> movies = new List<Movie>()
		{
			new Movie
			{
				MovieID = "1234567",
				Title = "Lord of the Rings, The Fellowship of the Ring",
				Genre = MovieGenre.adventure,
				Cert = Certification.Cert15A,
				ReleaseDate = Convert.ToDateTime("19/12/2001"),
				AverageRating = 8
			},
			new Movie
			{
				MovieID = "1234568",
				Title = "Lord of the Rings, The Two Towers",
				Genre = MovieGenre.adventure,
				Cert = Certification.Cert15A,
				ReleaseDate = Convert.ToDateTime("18/12/2002"),
				AverageRating = 9
			},
			new Movie
			{
				MovieID = "1234569",
				Title = "Lord of the Rings, The Return of the King",
				Genre = MovieGenre.adventure,
				Cert = Certification.Cert15A,
				ReleaseDate = Convert.ToDateTime("17/12/2003"),
				AverageRating = 10
			},
			new Movie
			{
				MovieID = "1234560",
				Title = "The Hobbit",
				Genre = MovieGenre.adventure,
				Cert = Certification.Cert15A,
				ReleaseDate = Convert.ToDateTime("13/12/2013"),
				AverageRating = 7
			},

		};

		// GET: api/<MovieController>
		// Return data about all movies in the catalogue, sorted in release date order (most recent to oldest).
		[HttpGet]
		public IEnumerable<Movie> Get()
		{

			movies = movies.OrderBy(x => x.ReleaseDate).ToList();
			return movies;
		}

		// GET api/<MovieController>/5
		// Return data about a specific movie as specified using a movie ID
		[HttpGet("{id}")]
		public IEnumerable<Movie> Get(string id)
		{
			var result = from s in movies
						 where s.MovieID.Equals(id)  // LINQ query method
						 select s;
			return result;
		}

		// Return a list of movie IDs and titles for movies that contain a specified keyword as a whole word in their title.
		[HttpGet("GetByKeyword/{keyword}")]
		public IEnumerable<Movie> GetByKeyword(string keyword)
		{
			var result = from s in movies
						 where s.Title.Contains(keyword)		// LINQ query method
						 select s;
			return result;		// Could not specify only Title and MovieID
		}
		// POST api/<MovieController>
		[HttpPost]
		public HttpStatusCode Post([FromBody] Movie value)
		{
			Movie found = movies.FirstOrDefault(p => p.MovieID.Equals(value.MovieID));
			if (found == null)
			{
				movies.Add(value);
				return HttpStatusCode.OK;
			}
			return HttpStatusCode.BadRequest;
		}

		// PUT api/<MovieController>/5
		[HttpPut("{id}")]
		public HttpStatusCode Put(string id, [FromBody] Movie value)
		{
			Movie found = movies.FirstOrDefault(p => p.MovieID.Equals(id));
			if (found != null)
			{
				found.Title = value.Title;
				found.Genre = value.Genre;
				found.Cert = value.Cert;
				found.ReleaseDate = value.ReleaseDate;
				found.AverageRating = value.AverageRating;
				return HttpStatusCode.OK;
			}
			return HttpStatusCode.NotFound;
		}
// PUT example
/*{
  "movieID": "string",
  "title": "Not LotR",
  "genre": 0,
  "cert": 0,
  "releaseDate": "2023-02-28T23:37:47.861Z",
  "averageRating": 5
}*/

	// DELETE api/<PhoneBookController>/5
	[HttpDelete("{id}")]
		public HttpStatusCode Delete(string id)
		{
			Movie found = movies.FirstOrDefault(p => p.MovieID.Equals(id));
			if (found != null)
			{
				movies.Remove(found);
				return HttpStatusCode.OK;
			}
			return HttpStatusCode.NotFound;
		}
	}
	
}
