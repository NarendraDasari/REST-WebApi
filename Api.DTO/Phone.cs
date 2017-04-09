using System.ComponentModel.DataAnnotations;

namespace Api.DomainObjects
{
    public class Phone
    {
        [StringLength(10,MinimumLength=10,ErrorMessage="Phone Number should be 10 digits")]
        public string Number { get; set; }
        public string Type { get; set; }
        
    }
    
    //LiteDb is failing to load the custom Override type of PhoneType. 

    //public class PhoneType
    //{
    //    private PhoneType(string value)
    //    {
    //        this.Type = value;
    //    }

    //    public string Type { get; set; }

    //    public static PhoneType Personal { get { return new PhoneType("Personal"); } }
    //    public static PhoneType Business { get { return new PhoneType("Business"); } }
    //    public static PhoneType Residential { get { return new PhoneType("Residential"); } }

    //    public override string ToString()
    //    {
    //        return this.Type;
    //    }
    //}

}
