namespace HeistBox.Data
{
    public  class Role
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Serious { get; set; }

        public Role(string name, string description, bool serious)
        {
            Name = name;
            Description = description;
            Serious = serious;
        }

        // Static list of roles with a serious flag
        public static List<Role> Roles = new List<Role>
    {
        // Core Heist Roles (Serious)
        new Role("Mastermind", "The planner who strategizes every step of the heist.", true),
        new Role("Leader", "Keeps the crew in check and makes critical decisions.", true),
        new Role("Hacker", "Disables security systems, hacks databases, and controls cameras.", true),
        new Role("Lockpicker", "Expert in cracking safes, picking locks, and bypassing security doors.", true),
        new Role("Driver", "The getaway expert, skilled in high-speed chases and evasive maneuvers.", true),
        new Role("Demolitions Expert", "Handles explosives for controlled breaches and distractions.", true),
        new Role("Con Artist", "Master of deception, disguise, and social engineering.", true),
        new Role("Muscle", "The enforcer who provides brute strength and intimidation.", true),
        new Role("Gunslinger", "Weapons expert, handles shootouts if things go sideways.", true),
        new Role("Acrobat", "Parkour specialist who scales buildings, squeezes through vents, and dodges lasers.", true),
        new Role("Tech Specialist", "Handles gadgets, security bypass tools, and high-tech gear.", true),
        new Role("Infiltrator", "Sneaky expert in stealth, disguises, and silent takedowns.", true),
        new Role("Scout", "Gathers intel, maps the location, and watches for patterns in security.", true),
        new Role("Inside Man", "Works within the target location, feeding the team key information.", true),

        // Silly/Fun Heist Roles (For Comedy)
        new Role("Distraction Guy", "Causes ridiculous distractions, from fake heart attacks to interpretive dance.", false),
        new Role("Lucky Fool", "Clueless, but somehow always ends up doing something helpful.", false),
        new Role("Animal Handler", "Uses pigeons, raccoons, or a trained capybara for heists.", false),
        new Role("Loud Guy", "Yells too much but still manages to be useful.", false),
        new Role("Intern", "Hired last minute, has no clue what’s happening.", false),
        new Role("Snack Coordinator", "Ensures the team stays fed during the heist.", false),
        new Role("Janitor", "Gets in with a mop and bucket, and no one questions them.", false),
        new Role("Fashionista", "Provides disguises, but insists on making everyone look fabulous.", false),
        new Role("Grandma", "Bakes cookies and distracts guards by asking for help crossing the street.", false),
        new Role("Guy Who Always Drops Something", "Sneaky… until they trip over air.", false),
        new Role("Unnecessary Acrobat", "Always flipping, even when unnecessary.", false),
        new Role("Hacker... But On Dial-Up", "Takes forever to hack anything.", false)
    };

        // Method to get a random role with a 2x higher chance of selecting a serious role
        public static Role GetRandomRole()
        {
            Random random = new Random();

            // Separate serious and silly roles
            List<Role> seriousRoles = Roles.FindAll(role => role.Serious);
            List<Role> sillyRoles = Roles.FindAll(role => !role.Serious);

            // Adjust the weight (serious roles will appear twice as often)
            List<Role> weightedRoles = new List<Role>();
            weightedRoles.AddRange(seriousRoles);
            weightedRoles.AddRange(seriousRoles);  // Add serious roles again to double their chance
            weightedRoles.AddRange(sillyRoles);

            // Select a random role from the weighted list
            int index = random.Next(weightedRoles.Count);
            return weightedRoles[index];
        }
    }
}
