-- Users
INSERT INTO [Users] (Id, FullName, Email, Role, PhoneNumber, FacebookUrl)
VALUES
('11111111-1111-1111-1111-111111111111', 'Alice Landlord', 'alice@landlord.com', 'Landlord', '1234567890', 'https://facebook.com/alice'),
('22222222-2222-2222-2222-222222222222', 'Bob Student', 'bob@student.com', 'Student', '2345678901', NULL),
('33333333-3333-3333-3333-333333333333', 'Carol Admin', 'carol@admin.com', 'Admin', '3456789012', NULL);

-- Listings
INSERT INTO [Listings] (Id, LandlordId, Title, Description, Location, PricePerMonth, NextAvailableDate, IsActive)
VALUES
('aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1', '11111111-1111-1111-1111-111111111111', 'Sunny Apartment', 'A bright and cozy apartment.', 'Downtown', 1200.00, '2024-07-01', 1),
('aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2', '11111111-1111-1111-1111-111111111111', 'Quiet Room', 'Perfect for students.', 'Campus Area', 800.00, NULL, 1);

-- Bookings
INSERT INTO [Bookings] (Id, ListingId, StudentId, MoveInDate, MoveOutDate, Status, StripeSessionId)
VALUES
('bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1', 'aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaa1', '22222222-2222-2222-2222-222222222222', '2024-07-05', '2024-08-05', 'Accepted', 'sess_12345'),
('bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbb2', 'aaaaaaa2-aaaa-aaaa-aaaa-aaaaaaaaaaa2', '22222222-2222-2222-2222-222222222222', '2024-09-01', NULL, 'Pending', NULL);

-- Reviews
INSERT INTO [Reviews] (Id, BookingId, Rating, Comment)
VALUES
('ccccccc1-cccc-cccc-cccc-ccccccccccc1', 'bbbbbbb1-bbbb-bbbb-bbbb-bbbbbbbbbbb1', 5, 'Great stay, very comfortable!');

