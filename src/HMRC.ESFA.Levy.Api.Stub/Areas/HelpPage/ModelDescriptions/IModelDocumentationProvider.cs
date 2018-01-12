using System;
using System.Reflection;

namespace HMRC.ESFA.Levy.Api.Stub.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}