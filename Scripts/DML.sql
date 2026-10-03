USE MinorProject2DB;

INSERT INTO Members (FirstName, LastName, Email, Phone) VALUES
('John', 'Smith', 'john.smith@123@', '555-1001'),
('Sarah', 'Lee', 'sarah.lee@123@', '555-1002'),
('Mike', 'Brown', 'mike.brown@123@', '555-1003'),
('Emily', 'Davis', 'emily.davis@123@', '555-1004'),
('Alex', 'Wilson', 'alex.wilson@123@', '555-1005'),
('Olivia', 'Martin', 'olivia.martin@123@', '555-1006'),
('Daniel', 'Garcia', 'daniel.garcia@123@', '555-1007'),
('Sophia', 'Anderson', 'sophia.anderson@123@', '555-1008'),
('Chris', 'Taylor', 'chris.taylor@123@', '555-1009'),
('Emma', 'Thomas', 'emma.thomas@123@', '555-1010');

INSERT INTO Memberships (MemberID, MembershipType, StartDate, EndDate) VALUES
(1, 'Premium', '2026-01-01', '2027-01-01'),
(2, 'Basic', '2026-02-01', '2027-02-01'),
(3, 'Premium', '2026-03-01', '2027-03-01'),
(4, 'Standard', '2026-01-15', '2027-01-15'),
(5, 'Basic', '2026-02-15', '2027-02-15'),
(6, 'Premium', '2026-03-15', '2027-03-15'),
(7, 'Standard', '2026-04-01', '2027-04-01'),
(8, 'Premium', '2026-04-15', '2027-04-15'),
(9, 'Basic', '2026-05-01', '2027-05-01'),
(10, 'Standard', '2026-05-15', '2027-05-15');

INSERT INTO Trainers (FirstName, LastName, Specialty, Email) VALUES
('James', 'Carter', 'Strength Training', 'james.carter@123@'),
('Lisa', 'Miller', 'Weight Loss', 'lisa.miller@123@'),
('Robert', 'Wilson', 'Bodybuilding', 'robert.wilson@123@'),
('Anna', 'Moore', 'Yoga', 'anna.moore@123@'),
('David', 'Taylor', 'Powerlifting', 'david.taylor@123@'),
('Jessica', 'Anderson', 'Cardio', 'jessica.anderson@123@'),
('Michael', 'Thomas', 'Strength Training', 'michael.thomas@123@'),
('Rachel', 'Jackson', 'Mobility', 'rachel.jackson@123@'),
('Kevin', 'White', 'Bodybuilding', 'kevin.white@123@'),
('Laura', 'Harris', 'Weight Loss', 'laura.harris@123@');

INSERT INTO WorkoutPlans (MemberID, TrainerID, WorkoutName, WorkoutDays, Goal) VALUES
(1, 1, 'Strength Builder', 4, 'Strength'),
(2, 2, 'Weight Loss Plan', 5, 'Fat Loss'),
(3, 3, 'Muscle Builder', 5, 'Muscle Gain'),
(4, 4, 'Flexibility Plan', 3, 'Mobility'),
(5, 5, 'Power Program', 4, 'Strength'),
(6, 6, 'Cardio Program', 5, 'Endurance'),
(7, 7, 'Strength Builder', 4, 'Strength'),
(8, 8, 'Mobility Program', 3, 'Flexibility'),
(9, 9, 'Muscle Builder', 5, 'Muscle Gain'),
(10, 10, 'Weight Loss Plan', 5, 'Fat Loss');

INSERT INTO Exercises (ExerciseName, MuscleGroup, Equipment, Difficulty) VALUES
('Bench Press', 'Chest', 'Barbell', 'Intermediate'),
('Squat', 'Legs', 'Barbell', 'Intermediate'),
('Deadlift', 'Back', 'Barbell', 'Advanced'),
('Bicep Curl', 'Biceps', 'Dumbbell', 'Beginner'),
('Tricep Pushdown', 'Triceps', 'Cable', 'Beginner'),
('Lat Pulldown', 'Back', 'Cable', 'Beginner'),
('Shoulder Press', 'Shoulders', 'Dumbbell', 'Intermediate'),
('Leg Extension', 'Legs', 'Machine', 'Beginner'),
('Leg Curl', 'Hamstrings', 'Machine', 'Beginner'),
('Cable Fly', 'Chest', 'Cable', 'Intermediate');