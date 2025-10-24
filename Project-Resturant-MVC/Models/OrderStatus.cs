namespace Project_Resturant_MVC.Models
{
    public enum OrderStatus
    {
        Pending,      // قيد الانتظار
        Preparing,    // جاري التحضير
        Ready,        // جاهز
        Delivered,    // تم التوصيل
        Cancelled     // ملغي
    }
}
