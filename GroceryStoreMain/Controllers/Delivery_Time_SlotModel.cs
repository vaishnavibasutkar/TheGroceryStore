using GroceryStoreMain.Models;
using System.ComponentModel.DataAnnotations;

namespace GroceryStoreMain.Models
{
    public class Delivery_Time_SlotModel
    {
        public Delivery_Time_SlotModel()
        {

        }
        public Delivery_Time_SlotModel(Delivery_Time_Slot delivery_Time_Slot)
        {
            this.dts_id = delivery_Time_Slot.dts_id;
            this.capacity = delivery_Time_Slot.capacity;
            this.name = delivery_Time_Slot.name;
            this.start_datetime = delivery_Time_Slot.start_datetime;
            this.duration = delivery_Time_Slot.duration;
        }

        public static Delivery_Time_Slot GetDelivery_Time_Slot(Delivery_Time_SlotModel delivery_Time_SlotModel)
        {
            Delivery_Time_Slot delivery_Time_Slot = new Delivery_Time_Slot();
            delivery_Time_Slot.dts_id = delivery_Time_SlotModel.dts_id;
            delivery_Time_Slot.capacity = delivery_Time_SlotModel.capacity;
            delivery_Time_Slot.name = delivery_Time_SlotModel.name;
            delivery_Time_Slot.start_datetime = delivery_Time_SlotModel.start_datetime;
            delivery_Time_Slot.duration = delivery_Time_SlotModel.duration;
            return delivery_Time_Slot;

        }
        public int dts_id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Capacity is Required")]
        [Display(Name = "Capacity")]
        public int capacity { get; set; }

        [Required(ErrorMessage = "Start Datetime is Required")]
        [Display(Name = "Name")]
        public System.DateTime start_datetime { get; set; }

        [Required(ErrorMessage = "Duration is Required")]
        [Display(Name = "Duration")]
        public int duration { get; set; }
    }
}