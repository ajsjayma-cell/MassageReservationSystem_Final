CREATE DATABASE IF NOT EXISTS massage_reservation_system;
USE massage_reservation_system;

CREATE TABLE users (
  user_id INT AUTO_INCREMENT PRIMARY KEY,
  full_name VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE,
  password VARCHAR(255) NOT NULL,
  role ENUM('customer','admin') DEFAULT 'customer',
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE services (
  service_id INT AUTO_INCREMENT PRIMARY KEY,
  service_name VARCHAR(100) NOT NULL,
  description TEXT,
  price DECIMAL(10,2) NOT NULL,
  duration INT NOT NULL
);

CREATE TABLE therapists (
  therapist_id INT AUTO_INCREMENT PRIMARY KEY,
  full_name VARCHAR(100) NOT NULL,
  specialization VARCHAR(100),
  availability_status ENUM('Available','Unavailable') DEFAULT 'Available'
);

CREATE TABLE reservations (
  reservation_id INT AUTO_INCREMENT PRIMARY KEY,
  user_id INT NOT NULL,
  service_id INT NOT NULL,
  therapist_id INT NOT NULL,
  reservation_date DATE NOT NULL,
  reservation_time TIME NOT NULL,
  status ENUM('Pending','Approved','Completed','Cancelled') DEFAULT 'Pending',
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
  FOREIGN KEY (service_id) REFERENCES services(service_id) ON DELETE CASCADE,
  FOREIGN KEY (therapist_id) REFERENCES therapists(therapist_id) ON DELETE CASCADE
);

INSERT INTO users (full_name, email, password, role) VALUES
('System Administrator', 'admin@example.com', '$2y$10$9IDcNf8dCT2ynFJTsQn6f.WgFRT.gWRfqmtunfw03jVZZd8d59P4u', 'admin');
-- default admin password: admin123

INSERT INTO services (service_name, description, price, duration) VALUES
('Swedish Massage', 'Relaxing full-body massage for stress relief.', 500.00, 60),
('Deep Tissue Massage', 'Targets deeper muscle layers and tension.', 700.00, 60),
('Hot Stone Massage', 'Warm stones used to relax muscles.', 900.00, 90),
('Aromatherapy Massage', 'Massage with essential oils for relaxation.', 650.00, 60);

INSERT INTO therapists (full_name, specialization, availability_status) VALUES
('Maria Santos', 'Swedish Massage', 'Available'),
('Juan Dela Cruz', 'Deep Tissue Massage', 'Available'),
('Ana Reyes', 'Aromatherapy Massage', 'Available');
