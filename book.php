<?php include 'header.php';
if (!is_logged_in()) redirect('login.php');
$message = '';
$services = $pdo->query("SELECT * FROM services ORDER BY service_name")->fetchAll(PDO::FETCH_ASSOC);
$therapists = $pdo->query("SELECT * FROM therapists WHERE availability_status='Available' ORDER BY full_name")->fetchAll(PDO::FETCH_ASSOC);
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $service_id = (int)$_POST['service_id'];
    $therapist_id = (int)$_POST['therapist_id'];
    $date = $_POST['reservation_date'];
    $time = $_POST['reservation_time'];
    $check = $pdo->prepare("SELECT COUNT(*) FROM reservations WHERE therapist_id=? AND reservation_date=? AND reservation_time=? AND status IN ('Pending','Approved')");
    $check->execute([$therapist_id,$date,$time]);
    if ($check->fetchColumn() > 0) {
        $message = '<div class="alert alert-danger">Selected therapist is already booked at this date and time.</div>';
    } else {
        $stmt = $pdo->prepare("INSERT INTO reservations(user_id,service_id,therapist_id,reservation_date,reservation_time) VALUES(?,?,?,?,?)");
        $stmt->execute([$_SESSION['user_id'],$service_id,$therapist_id,$date,$time]);
        $message = '<div class="alert alert-success">Reservation submitted successfully.</div>';
    }
}
$selectedService = isset($_GET['service_id']) ? (int)$_GET['service_id'] : '';
?>
<div class="container my-5"><div class="row justify-content-center"><div class="col-md-7">
<div class="card p-4"><h3>Book Appointment</h3><?php echo $message; ?>
<form method="post">
  <label>Service</label>
  <select class="form-select mb-3" name="service_id" required>
    <?php foreach($services as $s): ?><option value="<?php echo $s['service_id']; ?>" <?php echo $selectedService==$s['service_id']?'selected':''; ?>><?php echo clean($s['service_name']); ?> - ₱<?php echo number_format($s['price'],2); ?></option><?php endforeach; ?>
  </select>
  <label>Therapist</label>
  <select class="form-select mb-3" name="therapist_id" required>
    <?php foreach($therapists as $t): ?><option value="<?php echo $t['therapist_id']; ?>"><?php echo clean($t['full_name']); ?> - <?php echo clean($t['specialization']); ?></option><?php endforeach; ?>
  </select>
  <label>Date</label>
  <input class="form-control mb-3" type="date" name="reservation_date" min="<?php echo date('Y-m-d'); ?>" required>
  <label>Time</label>
  <input class="form-control mb-3" type="time" name="reservation_time" min="08:00" max="20:00" required>
  <button class="btn btn-main w-100">Submit Reservation</button>
</form></div></div></div></div>
<?php include 'footer.php'; ?>
