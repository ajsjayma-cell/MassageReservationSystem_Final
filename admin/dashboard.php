<?php include 'admin_header.php';
$users = $pdo->query("SELECT COUNT(*) FROM users WHERE role='customer'")->fetchColumn();
$services = $pdo->query("SELECT COUNT(*) FROM services")->fetchColumn();
$therapists = $pdo->query("SELECT COUNT(*) FROM therapists")->fetchColumn();
$reservations = $pdo->query("SELECT COUNT(*) FROM reservations")->fetchColumn();
?>
<div class="container my-5"><h2>Dashboard</h2><div class="row g-4 mt-2">
<div class="col-md-3"><div class="card p-4 text-center"><h3><?php echo $users; ?></h3><p>Customers</p></div></div>
<div class="col-md-3"><div class="card p-4 text-center"><h3><?php echo $services; ?></h3><p>Services</p></div></div>
<div class="col-md-3"><div class="card p-4 text-center"><h3><?php echo $therapists; ?></h3><p>Therapists</p></div></div>
<div class="col-md-3"><div class="card p-4 text-center"><h3><?php echo $reservations; ?></h3><p>Reservations</p></div></div>
</div><div class="mt-4"><a class="btn btn-main" href="reservations.php">Manage Reservations</a> <a class="btn btn-main" href="services.php">Manage Services</a> <a class="btn btn-main" href="therapists.php">Manage Therapists</a></div></div>
<?php include 'admin_footer.php'; ?>
