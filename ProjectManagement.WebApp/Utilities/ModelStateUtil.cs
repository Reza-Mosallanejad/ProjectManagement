using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectManagement.Domain.Common;

namespace ProjectManagement.WebApp.Utilities
{
    public static class ModelStateUtil
    {
        public static OperationResult IsValid(this ModelStateDictionary modelstate)
        {
            var opr = OperationResult.Success();
            if (!modelstate.IsValid)
            {
                var errors = modelstate.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                opr = OperationResult.Fail(string.Join("</br>", errors));
            }
            return opr;
        }
    }
}
