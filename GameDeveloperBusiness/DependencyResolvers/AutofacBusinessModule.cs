using Autofac;
using GameDeveloperBusiness.Abstract;
using GameDeveloperBusiness.Concrete;
using GameDeveloperDataAccess.Abstract;
using GameDeveloperDataAccess.Concrete;
using GameDeveloperEntity.Concrete;

namespace GameDeveloperBusiness.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AdminManager>().As<IAdminService>();
            builder.RegisterType<EfAdminDal>().As<IAdminDal>();

            builder.RegisterType<MatchManager>().As<IMatchService>();
            builder.RegisterType<EfQueueDal>().As<IQueueDal>();

            builder.Register(c => new JwtManager<User>()).As<IJwtService<User>>();
        }
    }
}
