using Ganss.Xss;
using Microsoft.Extensions.Primitives;

namespace MITC_Smart_Solution.Middleware
{
    public class SanitizerMiddleware(RequestDelegate next, IHtmlSanitizer htmlSanitizer)
    {
        private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
        private readonly IHtmlSanitizer _htmlSanitizer = htmlSanitizer ?? throw new ArgumentNullException(nameof(htmlSanitizer));

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.HasFormContentType)
            {
                var form = context.Request.Form;

                var sanitizedForm = new FormCollection(
                    form.ToDictionary(
                        kvp => kvp.Key,
                        kvp => new StringValues(
                            kvp.Value.Select(value => _htmlSanitizer.Sanitize(value).ToString()).ToArray()
                        )
                    )
                );

                context.Request.Form = sanitizedForm;
            }

            await _next(context);
        }

    }
}
