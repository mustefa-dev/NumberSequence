using Auth.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace NumberSequence.Helper
{
    public class OrderNumberGenerator
    {
        private int _currentOrderNumber;
        private readonly DataContext _context;
        private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public OrderNumberGenerator(DataContext context)
        {
            _context = context;
            InitializeCurrentOrderNumber().Wait();
        }

        private async Task InitializeCurrentOrderNumber()
        {
            var lastOrder = await _context.Orders.OrderByDescending(o => o.CreationDate).FirstOrDefaultAsync();
            _currentOrderNumber = lastOrder == null ? 0 : lastOrder.OrderNumber;
        }

        public async Task<int> GetNextOrderNumber()
        {
            await _semaphore.WaitAsync();
            try
            {
                _currentOrderNumber = _currentOrderNumber == 10 ? 1 : _currentOrderNumber + 1;
                return _currentOrderNumber;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}