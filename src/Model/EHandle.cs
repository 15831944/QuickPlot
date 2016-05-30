

namespace QuickPrint.Model
{
    struct EHandle
    {
        public int Grade;
        public int ParentOrder;
        public int Order;

        public EHandle(int grade, int parentOrder, int order)
        {
            Grade = grade;
            ParentOrder = parentOrder;
            Order = order;
        }

        public override string ToString()
        {
            return Grade.ToString() + " " + ParentOrder.ToString() + " " + Order.ToString();
        }
    }
}
