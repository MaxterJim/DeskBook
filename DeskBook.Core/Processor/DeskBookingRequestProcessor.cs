using DeskBook.Core.DataInterface;
using DeskBook.Core.Domain;

namespace DeskBook.Core.Processor
{
    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository _deskBookingRepository;
        private readonly IDeskRepository _deskRepository;

        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository, IDeskRepository deskRepository)
        {
            _deskBookingRepository = deskBookingRepository;
            _deskRepository = deskRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest? request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = Create<DeskBookingResult>(request);

            var availabelDesks = _deskRepository.GetAvailableDesks(request.Date);

            if (availabelDesks.FirstOrDefault() is Desk availableDesk) //Esta linea pregunta si hay un elemento en la lista y si es de tipo Desk si es asi lo almacena en la variable availableDesk
            {
                //var availableDesk = availabelDesks.First(); //Esta linea se puede eliminar ya que en el if lo hace automaticamente
                var deskBooking = Create<DeskBooking>(request);
                deskBooking.DeskId = availableDesk.Id;

                _deskBookingRepository.Save(deskBooking);

                result.DeskBookingId = deskBooking.Id;
                result.Code = DeskBookingResultCode.Success;
            }
            else
            {
                result.Code = DeskBookingResultCode.NoDeskAvailable;
            }
            return result;
        }

        /* 
         * Esto es un metodo generico que sirve tanto para DeskBookingRequest como para DeskBookingResult, ya que los dos son lo mismo ya que 
         * heredan de DeskBookinBase. Cambiamos el nombre a solo Create y le decimos que es con el parametro generico T que a su vez tambien 
         * devuelve T (T Create<T>), luego especificamos que T es un objeto de tipo DeskBookinBase (where T:DeskBookinBase) y le adicionamos 
         * el constructor predeterminado (new()).Despues tambien debemos poner una T en el return para que coincida con el valor de especificado 
         * de retorno (new T)
         */
        private static T Create<T>(DeskBookingRequest request) where T:DeskBookinBase, new() 
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}