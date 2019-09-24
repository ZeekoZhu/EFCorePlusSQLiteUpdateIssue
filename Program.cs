using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace EFCorePlusSQLiteUpdateIssue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var blogPosts = Enumerable.Range(0, 10).Select(i => new BlogPost());

            using (var db = new FooDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                void ListPosts()
                {
                    var inserted = db.BlogPosts.AsNoTracking().ToList().Select(x => x.ToString());
                    foreach (var value in inserted)
                    {
                        Console.WriteLine(value);
                    }
                }

                db.AddRange(blogPosts);
                db.SaveChanges();

                ListPosts();

                var p = new UpdateParams
                {
                    PostIds = Enumerable.Range(0, 3).ToList()
                };

                await db.BlogPosts.Where(x => p.PostIds.Contains(x.Id))
                    .UpdateAsync(
                        x => new BlogPost
                        {
                            ViewCount = 42
                        });
//                db.BlogPosts.Where(x => x.Id < 5).Update(x => new BlogPost
//                {
//                    ViewCount = x.ViewCount + 1
//                });

                ListPosts();
            }
        }

        class UpdateParams
        {
            public List<int> PostIds { get; set; }
        }
    }
}