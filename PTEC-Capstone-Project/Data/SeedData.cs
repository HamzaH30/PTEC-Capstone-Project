using PTEC_Capstone_Project.Models;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace PTEC_Capstone_Project.Data
{
    public class SeedData
    {
        const int NumPosts = 10;
        const int NumUsers = 40;

        public static async Task Initialize(IServiceProvider serviceProvider, string? seedUserPw)
        {
            if (seedUserPw == null)
            {
                throw new Exception("No seed password!");
            }

            await SeedSuperAdminUser(serviceProvider, seedUserPw);

            List<Task> seedDataTasks = new List<Task>();

            // Seed data that is independant on any othr data
            seedDataTasks.Add(SeedGames(serviceProvider));
            seedDataTasks.Add(SeedUsers(serviceProvider, seedUserPw));

            // Seed data that is dependant on the data above
            await Task.WhenAll(seedDataTasks);
            seedDataTasks.Add(SeedGroups(serviceProvider));


            // Once all the data has been seeded, update the database
            await Task.WhenAll(seedDataTasks);
            var dbContext = serviceProvider.GetService<ApplicationDbContext>()!;
            await dbContext.SaveChangesAsync();
        }

        public static async Task SeedPosts(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>()!;

            if (!dbContext.Posts.Any() && dbContext.Groups.Any() && dbContext.Games.Any())
            {
                List<Post> posts = new List<Post>();
                List<string> Descriptions = new List<string>()
                {
                    "Looking for a competitive group to play rank.\nWanting to level up to 2 new levels. Playing for maybe around 2 hours.",
                    "Casual gaming. We've been playing for a couple of years. We can help you level up.",
                    "Wanting to practice before Nationals.",
                    "Seeking expert players for late-night raids. Must have mic and good teamwork skills.",
                    "Weekend warriors needed for laid-back PvP sessions. Beginners welcome!",
                    "Join our questing group! Looking for two more players to complete our team. We play evenings EST.",
                    "Daily morning gamer seeking same for quick matches before work.",
                    "Need a fourth for our squad in shooter games. We focus on strategy and communication.",
                    "Looking for someone to coach me in FPS games. Willing to trade coaching in RTS games.",
                    "Weekday late afternoon group looking for one more player for a co-op campaign.",
                    "Seeking a partner for duo ranking. Must be serious about climbing the ladder and available evenings.",
                    "Hardcore gaming this Saturday. Starting a marathon session and need more players!",
                    "LF2M (Looking for 2 more) for a competitive team. Tryouts tonight at 7 PM.",
                    "Need a substitute player for our tournament next week. High skill required.",
                    "Exploring the world of newly released MMO. Join us for a fresh start and epic adventures.",
                    "Casual, friendly group looking to expand our horizons with new members. All games, all fun!",
                    "Strategy aficionados unite! Looking for group members interested in deep tactical gameplay.",
                    "Setting up a learning group for beginners. We focus on fun and steady improvement.",
                    "Creating a lore-focused group for deep dives into game stories and character arcs. Join us for discussions and gameplay.",
                    "Speedrunner looking for partners to practice and exchange tips with. Let’s break some records together!",
                    "Parents who play! Looking for other gaming parents for relaxed late evening sessions."
                };


                for (int i = 0; i < NumPosts; i++)
                {
                    Random rnd = new Random();

                    // Retrieve random game
                    int rndGameId = rnd.Next(0, dbContext.Games.Count());
                    Game randomGame = dbContext.Games.FirstOrDefault(g => g.Id == rndGameId);


                    Post post = new Post()
                    {
                        Timestamp = DateTime.Now,
                        Description = Descriptions[rnd.Next(0, Descriptions.Count)],
                        Game = randomGame,
                    };
                }
            }
        }

        public static async Task SeedUserPosts(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>()!;

            if (!dbContext.UserPosts.Any() && dbContext.Users.Any() && dbContext.Posts.Any())
            {
                List<UserPost> userPosts = new List<UserPost>();

                foreach (Post post in dbContext.Posts)
                {

                }
            }
        }

        /*
         * Based on previous discussion, the whole "Group" is redundant within the context of the MVP.
         * May 14, 2024
        public static async Task SeedGroups(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>()!;

            // Ensure you're only adding groups if there aren't any already and there are users to associate with.
            if (!dbContext.Groups.Any() && dbContext.Users.Any())
            {
                Random rnd = new Random();

                List<Group> groups = new List<Group>();

                for (int i = 0; i < NumPosts; i++)
                {
                    // Retrieve a random user to assign as the creator of the group
                    ApplicationUser randomUser = dbContext.Users.OrderBy(u => Guid.NewGuid()).FirstOrDefault()!;

                    // Retrieve a random game
                    int numGames = dbContext.Games.Count();
                    int rndGameId = rnd.Next(0, numGames);
                    Game randomGame = dbContext.Games.FirstOrDefault(g => g.Id == rndGameId);

                    groups.Add(new Group() { Capacity = rnd.Next(1, 5), CreationDate = DateTime.Now, ApplicationUser = randomUser, Game = randomGame });
                }

                await dbContext.Groups.AddRangeAsync(groups);
            }
        }
        */


        public static async Task SeedUsers(IServiceProvider serviceProvider, string? seedUserPw)
        {
            ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>()!;
            
            // Assuming the SuperAdmin user is already in
            if (dbContext.Users.Count() < 2 && !String.IsNullOrEmpty(seedUserPw))
            {
                for (int i = 1; i <= NumUsers; i++)
                {
                    await SeedUser(serviceProvider, $"SeedUser{i}", seedUserPw);
                }
            }
        }

        public static async Task SeedGames(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetService<ApplicationDbContext>()!;

            if (!dbContext.Games.Any())
            {
                List<Game> games = new List<Game>
                {
                    new Game { Title = "Elder Scrolls V: Skyrim", Genre = "Role-playing", Description = "Open world action role-playing video game."},
                    new Game { Title = "The Witcher 3: Wild Hunt", Genre = "Role-playing", Description = "Action role-playing game set in a fantasy world."},
                    new Game { Title = "Dark Souls III", Genre = "Action RPG", Description = "Action RPG known for its challenging difficulty."},
                    new Game { Title = "Red Dead Redemption 2", Genre = "Action-Adventure", Description = "Western-themed action-adventure game."},
                    new Game { Title = "The Legend of Zelda: Breath of the Wild", Genre = "Action-Adventure", Description = "Open world adventure with exploration and puzzle-solving elements."},
                    new Game { Title = "Portal 2", Genre = "Puzzle", Description = "Puzzle platformer known for its original gameplay mechanics and story."},
                    new Game { Title = "Minecraft", Genre = "Sandbox", Description = "Sandbox game that allows players to build and explore pixelated worlds."},
                    new Game { Title = "BioShock Infinite", Genre = "First-Person Shooter", Description = "First-person shooter with strong narrative and role-playing elements."},
                    new Game { Title = "FIFA 2023", Genre = "Sports", Description = "Latest installment in the FIFA series of soccer simulations."},
                    new Game { Title = "Overwatch", Genre = "First-Person Shooter", Description = "Team-based multiplayer first-person shooter."},
                    new Game { Title = "God of War", Genre = "Action", Description = "Action-adventure game based on ancient mythology."},
                    new Game { Title = "Fortnite", Genre = "Battle Royale", Description = "Survival game featuring a battle royale mode."},
                    new Game { Title = "Call of Duty: Modern Warfare", Genre = "First-Person Shooter", Description = "Military shooter with both single-player and multiplayer modes."},
                    new Game { Title = "Super Mario Odyssey", Genre = "Platform", Description = "3D platform game with exploration and puzzle elements."},
                    new Game { Title = "Grand Theft Auto V", Genre = "Action-Adventure", Description = "Open-world game that offers both single-player and online multiplayer modes."},
                    new Game { Title = "Celeste", Genre = "Platform", Description = "Platform game known for its challenging gameplay and touching story."},
                    new Game { Title = "Rocket League", Genre = "Sports", Description = "Vehicle soccer game with physics-based gameplay."},
                    new Game { Title = "The Sims 4", Genre = "Simulation", Description = "Life simulation game where you create and control people."},
                    new Game { Title = "League of Legends", Genre = "MOBA", Description = "Multiplayer online battle arena where players compete in team-based matches."},
                    new Game { Title = "Dota 2", Genre = "MOBA", Description = "A competitive game of action and strategy, played both professionally and casually."},
                    new Game { Title = "Halo: Infinite", Genre = "First-Person Shooter", Description = "Latest installment in the Halo sci-fi military series."},
                    new Game { Title = "Among Us", Genre = "Party", Description = "Multiplayer game where players work together while a few attempt to sabotage the team."},
                    new Game { Title = "Cyberpunk 2077", Genre = "Role-playing", Description = "Open-world RPG set in a dystopian future."},
                    new Game { Title = "Stardew Valley", Genre = "Simulation", Description = "Farming simulation game that also includes role-playing elements."},
                    new Game { Title = "Persona 5 Royal", Genre = "Role-playing", Description = "Japanese role-playing game with an emphasis on dungeon exploration and social simulation."},
                    new Game { Title = "Animal Crossing: New Horizons", Genre = "Simulation", Description = "A community simulation game where players develop an island and interact with anthropomorphic animals."},
                    new Game { Title = "Assassin's Creed Valhalla", Genre = "Action RPG", Description = "Open world action RPG set in the Viking era."},
                    new Game { Title = "Monster Hunter World", Genre = "Action RPG", Description = "Action RPG where players hunt and battle giant monsters in a vast environment."},
                    new Game { Title = "Borderlands 3", Genre = "First-Person Shooter", Description = "Looter shooter with a vast array of guns and an emphasis on multiplayer cooperation."},
                    new Game { Title = "The Last of Us Part II", Genre = "Action-Adventure", Description = "Action-adventure game with a strong narrative focused on character development and emotional storytelling."},
                    new Game { Title = "Mass Effect: Andromeda", Genre = "Action RPG", Description = "Sci-fi role-playing game with an emphasis on exploration and narrative."},
                    new Game { Title = "Hitman 3", Genre = "Stealth", Description = "Stealth game focusing on assassination missions across global locations."},
                    new Game { Title = "Civilization VI", Genre = "Strategy", Description = "Turn-based strategy game where players build and expand a civilization through the ages."},
                    new Game { Title = "Gears 5", Genre = "Third-Person Shooter", Description = "Third-person shooter focusing on high action and deep storytelling."},
                    new Game { Title = "Bloodborne", Genre = "Action RPG", Description = "Dark and challenging action RPG set in a gothic world."},
                    new Game { Title = "Cuphead", Genre = "Run and Gun", Description = "Run and gun platformer known for its 1930s cartoon art style and challenging difficulty."},
                    new Game { Title = "Sekiro: Shadows Die Twice", Genre = "Action-Adventure", Description = "Action-adventure game that focuses on stealth, exploration, and combat with a feudal Japan setting."},
                    new Game { Title = "Horizon Zero Dawn", Genre = "Action RPG", Description = "Action RPG set in a post-apocalyptic world with robotic creatures."},
                    new Game { Title = "Destiny 2", Genre = "First-Person Shooter", Description = "Multiplayer first-person shooter set in a mythic science fiction world."},
                    new Game { Title = "Fallout 4", Genre = "Action RPG", Description = "Post-apocalyptic role-playing game with a massive open world."},
                    new Game { Title = "Baldur's Gate 3", Genre = "Role-playing", Description = "Role-playing game based on the Dungeons & Dragons tabletop role-playing system."},
                    new Game { Title = "Shadow of the Tomb Raider", Genre = "Action-Adventure", Description = "Action-adventure game focusing on Lara Croft's adventures."},
                    new Game { Title = "Far Cry 6", Genre = "First-Person Shooter", Description = "First-person shooter set in a fictional Caribbean island under the rule of a dictator."},
                    new Game { Title = "Metal Gear Solid V: The Phantom Pain", Genre = "Action-Adventure", Description = "Stealth game focusing on tactical freedom in how missions are completed."},
                    new Game { Title = "XCOM 2", Genre = "Strategy", Description = "Strategy game where players command a global defense force against an alien invasion."},
                    new Game { Title = "Street Fighter V", Genre = "Fighting", Description = "Competitive fighting game featuring a wide range of characters and styles."},
                    new Game { Title = "Control", Genre = "Action-Adventure", Description = "Supernatural action-adventure game with an emphasis on environmental exploration and psychic powers."},
                    new Game { Title = "Nier: Automata", Genre = "Action RPG", Description = "Action RPG set in a post-apocalyptic world, known for its narrative and multiple endings."},
                    new Game { Title = "Resident Evil 2 Remake", Genre = "Survival Horror", Description = "Remake of the classic survival horror game with updated graphics and gameplay."},
                    new Game { Title = "Death Stranding", Genre = "Action", Description = "Action game set in an open world with a focus on exploration and survival elements."},
                    new Game { Title = "Diablo III", Genre = "Action RPG", Description = "Action RPG with a focus on dungeon crawling and loot."},
                    new Game { Title = "Tetris Effect", Genre = "Puzzle", Description = "Puzzle game that is a reinvention of the classic Tetris with a modern twist."},
                    new Game { Title = "Splatoon 2", Genre = "Third-Person Shooter", Description = "Team-based third-person shooter with a focus on controlling territory with ink."},
                    new Game { Title = "Total War: Three Kingdoms", Genre = "Strategy", Description = "Strategy game set in ancient China, focusing on building an empire and controlling armies."},
                    new Game { Title = "Uncharted 4: A Thief's End", Genre = "Action-Adventure", Description = "Action-adventure game focusing on treasure hunting and high-octane action."},
                    new Game { Title = "The Outer Worlds", Genre = "Action RPG", Description = "Sci-fi role-playing game focusing on player-driven choices and narrative."},
                    new Game { Title = "Kingdom Hearts III", Genre = "Action RPG", Description = "Action RPG that mixes Disney characters and settings with original game characters."},
                    new Game { Title = "No Man's Sky", Genre = "Survival", Description = "Open universe survival game focused on exploration and trading."},
                    new Game { Title = "Battlefield V", Genre = "First-Person Shooter", Description = "First-person shooter set during World War II with a focus on large-scale multiplayer combat."},
                    new Game { Title = "Forza Horizon 4", Genre = "Racing", Description = "Open-world racing game set in a fictional representation of the United Kingdom."}
                };
                await dbContext.Games.AddRangeAsync(games);
            }
        }

        /// <summary>
        /// A specific method meant to create a super admin role
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="seedUserPw"></param>
        /// <returns></returns>
        public static async Task SeedSuperAdminUser(IServiceProvider serviceProvider, string? seedUserPw)
        {
            string superAdminUserName = "SuperAdmin";
            string adminId = await SeedUser(serviceProvider, superAdminUserName, seedUserPw);

            await SeedUserRole(serviceProvider, adminId, Constants.SuperAdminRole);
        }

        /// <summary>
        /// Method to create a seed user.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> SeedUser(IServiceProvider serviceProvider, string userName, string password)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;

            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userName,
                };

                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create seed user");
                }
            }

            return user.Id;
        }

        /// <summary>
        /// This is a method that creates a role, if it does not exist. Then will assign a user to that role.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<IdentityResult> SeedUserRole(IServiceProvider serviceProvider, string userId, string roleName)
        {
            // Check if role exists, if not, create it
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>()!;

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Assign user to this role
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;
            var user = await userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception("Seed user not found");
            IdentityResult result = await userManager.AddToRoleAsync(user, roleName);

            return result;
        }
    }
}
