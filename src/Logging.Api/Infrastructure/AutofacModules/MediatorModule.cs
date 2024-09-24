using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using Sofisoft.Enterprise.Logging.Application.Behaviors;
using Sofisoft.Enterprise.Logging.Application.Commands;
using Sofisoft.Enterprise.Logging.Application.Validations;

namespace Sofisoft.Enterprise.Logging.Api.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(CreateErrorCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            
            builder.RegisterAssemblyTypes(typeof(CreateErrorValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object? o; return (componentContext.TryResolve(t, out o) ? o : null)!; };
            });

            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}