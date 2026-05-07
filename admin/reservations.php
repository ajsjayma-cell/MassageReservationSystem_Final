<?php include 'admin_header.php';
if (isset($_POST['update_status'])) {
    $stmt = $pdo->prepare("UPDATE reservations SET status=? WHERE reservation_id=?");
    $stmt->execute([$_POST['status'], (int)$_POST['reservation_id']]);
}
$res = $pdo->query("SELECT r.*, u.full_name AS customer, s.service_name, t.full_name AS therapist FROM reservations r JOIN users u ON r.user_id=u.user_id JOIN services s ON r.service_id=s.service_id JOIN therapists t ON r.therapist_id=t.therapist_id ORDER BY r.created_at DESC")->fetchAll(PDO::FETCH_ASSOC);
?>
<div class="container my-5"><h2>Manage Reservations</h2><div class="table-responsive"><table class="table table-bordered align-middle"><thead><tr><th>Customer</th><th>Service</th><th>Therapist</th><th>Date</th><th>Time</th><th>Status</th><th>Action</th></tr></thead><tbody>
<?php foreach($res as $r): ?><tr><td><?php echo clean($r['customer']); ?></td><td><?php echo clean($r['service_name']); ?></td><td><?php echo clean($r['therapist']); ?></td><td><?php echo $r['reservation_date']; ?></td><td><?php echo date('h:i A', strtotime($r['reservation_time'])); ?></td><td><?php echo $r['status']; ?></td><td><form method="post" class="d-flex gap-2"><input type="hidden" name="reservation_id" value="<?php echo $r['reservation_id']; ?>"><select name="status" class="form-select form-select-sm"><option>Pending</option><option>Approved</option><option>Completed</option><option>Cancelled</option></select><button name="update_status" class="btn btn-sm btn-main">Update</button></form></td></tr><?php endforeach; ?>
</tbody></table></div></div><?php include 'admin_footer.php'; ?>
