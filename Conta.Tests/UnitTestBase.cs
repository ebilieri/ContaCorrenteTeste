namespace Conta.Tests
{
    public abstract class UnitTestBase
    {
        private volatile static bool autoMapperInitialized = false;
        private static readonly object lockObject = new object();

        public UnitTestBase()
        {
            InitializeAutomapper();
        }

        private static void InitializeAutomapper()
        {
            lock (lockObject)
            {
                if (!autoMapperInitialized)
                {
                    AutoMapper.Mapper.Initialize((cfg) =>
                    {
                        cfg.AddProfile<Application.Mappings.MappingProfile>();
                    });
                    autoMapperInitialized = true;
                }
            }
        }
    }
}
