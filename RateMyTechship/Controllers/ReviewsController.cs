using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RateMyTechship.Data;
using RateMyTechship.Models;

namespace RateMyTechship.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Review.ToListAsync());
        }

        //public async Task<IActionResult> Edit(int id, bool like)
        //{
        //    var review = await _context.Review.FindAsync(id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }

        //    if (like)
        //    {
        //        review.Likes++;
        //    }
        //    else
        //    {
        //        review.Dislike++;
        //    }

        //    await _context.SaveChangesAsync();

        //    // Return the updated review
        //    return Json(review);
        //}

        [HttpPost]
        [Authorize] // This ensures the user must be logged in
        public async Task<IActionResult> Like(int reviewId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = await _context.Review.FindAsync(reviewId);

            if (review == null)
            {
                return NotFound();
            }

            if (review.LikedByUserIds.Contains(userId))
            {
                review.LikedByUserIds.Remove(userId);
                review.Likes--;
            }
            else
            {

                review.LikedByUserIds.Add(userId);
                review.Likes++;
            }

            _context.SaveChanges();

            return Json(new { success = true, likesCount = review.Likes, likedByCurrentUser = review.HasLiked });
        }

        [HttpPost]
        [Authorize] // This ensures the user must be logged in
        public async Task<IActionResult> Dislike(int reviewId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = await _context.Review.FindAsync(reviewId);

            if (review == null)
            {
                return NotFound();
            }


            if (review.DislikedByUserIds.Contains(userId))
            {
                review.DislikedByUserIds.Remove(userId);
                review.Dislike--;
            }
            else
            {

                review.DislikedByUserIds.Add(userId);
                review.Dislike++;
            }

            _context.SaveChanges();

            return Json(new { success = true, dislikesCount = review.Dislike, dislikedByCurrentUser = review.HasDisliked });
        }

        // GET: Reviews/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View(await _context.Review.ToListAsync());
        }

        // POST: Reviews/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            var searchResults = await _context.Review
                .Where(r => r.InternshipReview.Contains(SearchPhrase) ||
                            r.CompanyName.Contains(SearchPhrase) ||
                            r.Position.Contains(SearchPhrase))
                .ToListAsync();

            if (searchResults.Count == 0)
            {
                ViewData["Message"] = "No companies found. Write a review!";
            }
            else
            {
                ViewData["Message"] = ""; // Clear the message if there are search results
            }

            return View("Index", searchResults);
        }

        [Authorize(Roles = "Admin")]
        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.ID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CreationDate,CompanyName,Duration,Position,Rating,InternshipReview,WorkCulture,LearningOpportunities,NetworkingOpportunities,Workload")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.CreationDate = DateTime.Now.ToString("MMMM dd, yyyy");
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CreationDate,CompanyName,Duration,Position,Rating,InternshipReview,WorkCulture,LearningOpportunities,NetworkingOpportunities,Workload")] Review review)
        {
            if (id != review.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingReview = await _context.Review.FindAsync(id);
                    if (existingReview == null)
                    {
                        return NotFound();
                    }

                    // Copy the values from the edited review to the existing one,
                    // excluding the Likes and Dislike properties
                    existingReview.CompanyName = review.CompanyName;
                    existingReview.Duration = review.Duration;
                    existingReview.Position = review.Position;
                    existingReview.Rating = review.Rating;
                    existingReview.InternshipReview = review.InternshipReview;
                    existingReview.WorkCulture = review.WorkCulture;
                    existingReview.LearningOpportunities = review.LearningOpportunities;
                    existingReview.NetworkingOpportunities = review.NetworkingOpportunities;
                    existingReview.Workload = review.Workload;

                    // Update the existing review in the database
                    _context.Update(existingReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }


        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,CreationDate,CompanyName,Duration,Position,Rating,InternshipReview,WorkCulture,LearningOpportunities,NetworkingOpportunities,Workload")] Review review)
        //{
        //    if (id != review.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        review.CreationDate = DateTime.Now.ToString("MMMM dd, yyyy");
        //        try
        //        {
        //            _context.Update(review);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ReviewExists(review.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(review);
        //}

        [Authorize(Roles = "Admin")]
        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.ID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [Authorize(Roles = "Admin")]
        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review != null)
            {
                _context.Review.Remove(review);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.ID == id);
        }
    }
}
