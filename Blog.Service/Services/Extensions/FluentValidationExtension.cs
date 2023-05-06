using FluentValidation.Results;// ValidationResult için bu sınıfı kullandık
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Extensions
{
    public static class FluentValidationExtension
    {
        //Doğrulama başarısız olursa, son kullanıcıya görüntülenebilmeleri için hata mesajlarını görünüme geri iletmemiz gerekir.ValidationResultBunu, FluentValidation türü için hata mesajlarını ASP.NET sözlüğüne kopyalayan bir uzantı yöntemi tanımlayarak yapabiliriz ModelState:Bu yöntem, aşağıdaki
        //örnekte denetleyici eyleminin içinde çağrılır.
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
