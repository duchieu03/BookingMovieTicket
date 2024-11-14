document.addEventListener("DOMContentLoaded", function () {
    const movieOptions = document.getElementById("movieOptions");
    const slotsSelect = document.querySelector("select[name='slots']");

    movieOptions.addEventListener("change", function () {
        const ticketInfo = document.querySelector('.buy-ticket-box ul li span#seats');
        const priceDisplay = document.querySelector('.buy-ticket-box ul li span#price');
        const seatTable = document.querySelector('.ticket-table-seat');

        seatTable.innerHTML = '';
        priceDisplay.textContent = '';
        ticketInfo.textContent = '';

        const movieId = this.value;
        fetch(`/Showtimes?handler=Showtimes&movieId=${movieId}`)
            .then(response => response.json())
            .then(data => {
                slotsSelect.innerHTML = '<option value="">Select a slot</option>';
                data.forEach(showtime => {
                    const showtimeDate = new Date(showtime.startTime);
                    const formattedTime = showtimeDate.getHours().toString().padStart(2, '0') + ':' + showtimeDate.getMinutes().toString().padStart(2, '0');
                    const option = new Option(formattedTime, showtime.showtimeId);
                    slotsSelect.appendChild(option);
                });
            })
            .catch(error => console.error('Error fetching showtimes:', error));
    });
});

function loadSeat() {
    const slotsSelect = document.querySelector("select[name='slots']");
    const showtimeId = slotsSelect.value;
    const selectedOption = slotsSelect.options[slotsSelect.selectedIndex];
    const roomId = selectedOption.getAttribute('value');
    const ticketInfo = document.querySelector('.buy-ticket-box ul li span#seats');
    const priceDisplay = document.querySelector('.buy-ticket-box ul li span#price');
    const movieDate = document.getElementById("movieDate").value;

    priceDisplay.textContent = '';
    ticketInfo.textContent = '';

    if (roomId && movieDate) {
        loadSeatsForRoom(roomId, movieDate);

        if (showtimeId) {
            fetch(`/Showtimes?handler=Price&showtimeId=${showtimeId}`)
                .then(response => response.json())
                .then(data => {
                    showtimePrice = data.price;
                })
                .catch(error => console.error('Error fetching price:', error));
        }
    } else {
        const seatTable = document.querySelector('.ticket-table-seat');
        seatTable.innerHTML = '';
    }
}

let showtimePrice = 0;
document.addEventListener("DOMContentLoaded", function () {
    const slotsSelect = document.querySelector("select[name='slots']");
    const movieDate = document.getElementById("movieDate");

    slotsSelect.addEventListener("change", loadSeat);
    movieDate.addEventListener("change", loadSeat);
});

function loadSeatsForRoom(roomId, movieDate) {
    fetch(`/Seats?handler=SeatsByRoomId&roomId=${roomId}&date=${movieDate}`)
        .then(response => response.json())
        .then(data => {
            const seatTable = document.querySelector('.ticket-table-seat');
            seatTable.innerHTML = '';
            let row = seatTable.insertRow(-1);
            data.forEach((seat, index) => {
                if (index % 10 === 0 && index !== 0) {
                    row = seatTable.insertRow(-1);
                }
                const cell = row.insertCell(-1);
                cell.textContent = seat.seatNumber;
                if (seat.isBooked) {
                    cell.classList.add('booked');
                } else {
                    cell.classList.add('clickable');
                    cell.addEventListener('click', function () {
                        this.classList.toggle('booked');
                        addSeatToTicket(seat.seatNumber);
                    });
                }
            });
        })
        .catch(error => console.error('Error loading seats:', error));
}
function addSeatToTicket(seatNumber) {
    const ticketInfo = document.querySelector('.buy-ticket-box ul li span#seats');
    const priceDisplay = document.querySelector('.buy-ticket-box ul li span#price');

    if (ticketInfo) {
        let selectedSeats = ticketInfo.textContent.trim().split(', ').filter(s => s);

        if (selectedSeats.includes(seatNumber)) {
            selectedSeats = selectedSeats.filter(s => s !== seatNumber);
        } else {
            selectedSeats.push(seatNumber);
        }

        ticketInfo.textContent = selectedSeats.join(', ');

        const totalPrice = selectedSeats.length * showtimePrice;
        priceDisplay.textContent = `$${totalPrice.toFixed(2)}`;
    }
}

function submitBookingForm() {
    const movieDate = document.getElementById("movieDate").value;
    const movieOptions = document.getElementById("movieOptions");
    const movieId = movieOptions.value;
    const slotsSelect = document.querySelector("select[name='slots']");
    const showtimeId = slotsSelect.value;
    const seats = document.querySelector('.buy-ticket-box ul li span#seats').textContent;
    const price = document.querySelector('.buy-ticket-box ul li span#price').textContent.replace('$', '');

    document.getElementById("formMovieDate").value = movieDate;
    document.getElementById("formMovieId").value = movieId;
    document.getElementById("formShowtimeId").value = showtimeId;
    document.getElementById("formSeats").value = seats;
    document.getElementById("formPrice").value = price;

    document.getElementById("bookingForm").submit();
}

function toggleDetails(bookingId) {
    var detailsRow = document.getElementById('details-' + bookingId);
    if (detailsRow.style.display === 'none') {
        detailsRow.style.display = '';
    } else {
        detailsRow.style.display = 'none';
    }
}

//signalR
var connection = new signalR.HubConnectionBuilder().withUrl("/documentHub").build();

connection.on("ReloadDocuments", function () {
    location.reload();
});

connection.start().then(
).catch(
    function (err) {
        return console.error(err.toString());
    }
);
