
Users
 - Id
 - FullName
 - Email => Unique
 - Address
 - Gender
 - IsDisabled
 - CreatedAt


Doctors (Doctor profile)
 - Id
 - User
 - Phone
 - Specialty
 - Picture
 - Bio
 - Certificate
 - TicketPrice
 - IsVerified => false
 - CreatedAt


Schedule
 - Id
 - Day
 - Location (Hospital)
 - Doctor
 - IsAvailable
 - CreatedAt


TimeSlots
 - Id
 - Schedule
 - StartTime
 - EndTime
 - Description
 - MaxAppointments
 - CreatedAt


Bookings
 - Id
 - User
 - AppointmentTime: DateTime => Unique per doctor.
 - TimeSlot
 - PaidAmount
 - Commission
 - DoctorRevenue
 - PaymentMethod
 - TransactionId
 - IsCompleted
 - CreatedAt


BookingNotes
 - Id
 - Booking
 - Note
 - CreatedAt


Reviews
 - Id
 - Booking
 - Stars (1 - 5)
 - Remarks
 - CreatedAt
