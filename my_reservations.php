<?php include 'header.php';
if (!is_logged_in()) redirect('login.php');
$stmt = $pdo->prepare("SELECT r.*, s.service_name, t.full_name AS therapist FROM reservations r JOIN services s ON r.service_id=s.service_id JOIN therapists t ON r.therapist_id=t.therapist_id WHERE r.user_id=? ORDER BY r.reservation_date DESC, r.reservation_time DESC");
$stmt->execute([$_SESSION['user_id']]);
$reservations = $stmt->fetchAll(PDO::FETCH_ASSOC);
?>
<div class="container my-5"><h2>My Reservations</h2><div class="table-responsive"><table class="table table-bordered">
<thead><tr><th>Service</th><th>Therapist</th><th>Date</th><th>Time</th><th>Status</th></tr></thead><tbody>
<?php foreach($reservations as $r): ?><tr>
<td><?php echo clean($r['service_name']); ?></td><td><?php echo clean($r['therapist']); ?></td><td><?php echo $r['reservation_date']; ?></td><td><?php echo date('h:i A', strtotime($r['reservation_time'])); ?></td><td><?php echo $r['status']; ?></td>
</tr><?php endforeach; ?>
</tbody></table></div></div>
<?php include 'footer.php'; ?>
