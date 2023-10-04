using System.Configuration;

namespace PillarEnecryptionForm
{
    public class PaillierCryptosystemDto
    {
        public uint Uoiler { get; set; }
        public uint Lamda { get; set; }
        public uint Module { get; set; }
        public uint ModuleSqure { get; set; }
        public uint G { get; set; }

        //public PaillierCryptosystemDto(
        //    uint uoiler,
        //    uint lamda,
        //    uint module,
        //    uint g
        //    )
        //{
        //    Uoiler = uoiler;
        //    Lamda = lamda;
        //    Module = module;
        //    G = g;
        //}
        public PaillierCryptosystemDto()
        {
            
        }
        public PaillierCryptosystemDto(
            uint uoiler,
            uint lamda,
            uint module,
        uint g

            )
        {

            Uoiler = uoiler;
            Lamda = lamda;
            Module = module;
            ModuleSqure = module * module;
            G = g;
        }
    }
}