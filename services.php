<?php include 'header.php';
$services = $pdo->query("SELECT * FROM services ORDER BY service_name")->fetchAll(PDO::FETCH_ASSOC);
?>
<div class="container my-5"><h2 class="mb-4">Massage Services</h2><div class="row g-4">
<?php foreach($services as $s): ?>
<div class="col-md-3"><div class="card p-3 h-100">
  <h5><?php echo clean($s['service_name']); ?></h5>
  <p><?php echo clean($s['description']); ?></p>
  <p><strong>₱<?php echo number_format($s['price'],2); ?></strong> / <?php echo $s['duration']; ?> mins</p>
  <?php if(is_logged_in()): ?><a class="btn btn-main" href="book.php?service_id=<?php echo $s['service_id']; ?>">Book</a><?php endif; ?>
</div></div>
<?php endforeach; ?>
</div></div>
<?php include 'footer.php'; ?>
