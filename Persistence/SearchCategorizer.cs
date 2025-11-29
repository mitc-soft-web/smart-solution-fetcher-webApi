namespace MITC_Smart_Solution.Persistence
{
    public class SearchCategorizer
    {
        public string Classify(string query)
        {
            query = query.ToLower();

            // Programming languages
            string[] languages = {
            "c#", "csharp", "html", "css", "javascript", "typescript", "python", "java", "kotlin",
            "swift", "php", "ruby", "go", "rust", "dart", "flutter", "c++", "c", "nodejs"
            };

            if (languages.Any(q => query.Contains(q))) return "software";

            // Frameworks
            string[] frameworks = {
            "asp.net", "blazor", "react", "angular", "vue",
            "django", "flask", "spring", "node", "express", "laravel", "next.js", "Tailwind"


            };

            if (frameworks.Any(q => query.Contains(q))) return "software";

            // Bugs, errors, packages, modules
            if (query.Contains("error") || query.Contains("exception")
                || query.Contains("bug") || query.Contains("install")
                || query.Contains("package") || query.Contains("dependency"))
                return "software";

            //Web design / UI / CMS / Dashboard
            if (query.Contains("figma") || query.Contains("ui") || query.Contains("ux")
                || query.Contains("dashboard") || query.Contains("wordpress")
                || query.Contains("cms") || query.Contains("template"))
                    return "webdesign";

            // Plumbing
            string[] plumbingTerms = { "pipe", "faucet", "drain", "valve", "leak", "water heater", "toilet",
                "shower", "bathtub", "sewage", "borehole"
            };
            if(plumbingTerms.Any(q => query.Contains(q))) return "plumbing";

            string[] networking =
            {
                "wifi", "router", "internet", "intranet", "ethernet", "modem", "network", "vpn",
                "computer repairs", "mars", "access point", "ip config", "laptop", "printer setup",
                "windows installation", "application installation"
            };

            if(networking.Any(q => query.Contains(q))) 
                return "computer hardware repairs, maintenance and networking";

            // Construction / Building
            if (query.Contains("cement") || query.Contains("concrete")
                || query.Contains("building") || query.Contains("block") || query.Contains("deck")
                || query.Contains("roof") || query.Contains("construction") || query.Contains("masonry")
                || query.Contains("plaster") || query.Contains("flooring") || query.Contains("plan")
                || query.Contains("tiling"))
                return "construction";
            
            // Electrical
            if (query.Contains("wire") || query.Contains("voltage")
            || query.Contains("circuit") || query.Contains("electri") || query.Contains("motor")
            || query.Contains("switch") || query.Contains("socket") || query.Contains("fuse")
            || query.Contains("capacitor") || query.Contains("transformer") || query.Contains("current"))
                return "electrical";

            return "general";
        }

    }
}
