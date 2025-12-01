using System.Text.RegularExpressions;

namespace MITC_Smart_Solution.Infrastructure
{
    public class SearchCategorizer
    {
        public string Classify(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return "general";

            query = query.ToLower();

            bool ContainsWord(string[] words)
            {
                foreach (var word in words)
                {
                    if (Regex.IsMatch(query, $@"\b{Regex.Escape(word.ToLower())}\b"))
                        return true;
                }
                return false;
            }

            // Plumbing 
            string[] plumbingTerms = { "pipe", "faucet", "drain", "valve", "leak", "water heater", "toilet",
                "shower", "bathtub", "sewage", "borehole" 
            };
            if (ContainsWord(plumbingTerms)) return "plumbing";

            // Networking / Computer hardware
            string[] networking = {
                "wifi", "router", "internet", "intranet", "ethernet", "modem", "network", "vpn",
                "computer repairs", "mars", "access point", "ip config", "laptop", "printer setup",
                "windows installation", "application installation"
            };
            if (ContainsWord(networking)) return "computer hardware repairs, maintenance and networking";

            // Electrical
            string[] electrical = {
                "wire", "voltage", "circuit", "electri", "motor",
                "switch", "socket", "fuse", "capacitor", "transformer", "current"
            };
            if (ContainsWord(electrical)) return "electrical";

            // Construction / Building
            string[] construction = {
                "cement", "concrete", "building", "block", "deck",
                "roof", "construction", "masonry", "plaster", "flooring",
                "plan", "tiling"
            };
            if (ContainsWord(construction)) return "construction";

            // Web Design / UI / CMS / Dashboard
            string[] webDesign = { "figma", "ui", "ux", "dashboard", "wordpress", "cms", "template" };
            if (ContainsWord(webDesign)) return "webdesign";

            // Programming Languages
            string[] languages = {
                "c#", "csharp", "html", "css", "javascript", "typescript", "python", "java", "kotlin",
                "swift", "php", "ruby", "go", "rust", "dart", "flutter", "c++", "c", "nodejs"
            };
            if (ContainsWord(languages)) return "software";

            // Frameworks
            string[] frameworks = {
                "asp.net", "blazor", "react", "angular", "vue",
                "django", "flask", "spring", "node", "express", "laravel", "next.js", "tailwind"
            };
            if (ContainsWord(frameworks)) return "software";

            // Bugs / Errors / Packages
            if (Regex.IsMatch(query, @"\b(error|exception|bug|install|package|dependency)\b")) return "software";

            // Default / general category
            return "general";
        }

    }
}
