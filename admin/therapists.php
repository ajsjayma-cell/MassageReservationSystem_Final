<?php include 'admin_header.php';
if (isset($_POST['add'])) {
    $stmt = $pdo->prepare("INSERT INTO therapists(full_name,specialization,availability_status) VALUES(?,?,?)");
    $stmt->execute([clean($_POST['full_name']), clean($_POST['specialization']), $_POST['availability_status']]);
}
if (isset($_GET['delete'])) {
    $stmt = $pdo->prepare("DELETE FROM therapists WHERE therapist_id=?");
    $stmt->execute([(int)$_GET['delete']]);
}
$items = $pdo->query("SELECT * FROM therapists ORDER BY full_name")->fetchAll(PDO::FETCH_ASSOC);
?>
<div class="container my-5"><h2>Manage Therapists</h2><div class="card p-4 mb-4"><form method="post" class="row g-3"><div class="col-md-4"><input class="form-control" name="full_name" placeholder="Full Name" required></div><div class="col-md-4"><input class="form-control" name="specialization" placeholder="Specialization"></div><div class="col-md-2"><select class="form-select" name="availability_status"><option>Available</option><option>Unavailable</option></select></div><div class="col-md-2"><button name="add" class="btn btn-main w-100">Add</button></div></form></div><table class="table table-bordered"><thead><tr><th>Name</th><th>Specialization</th><th>Status</th><th></th></tr></thead><tbody><?php foreach($items as $i): ?><tr><td><?php echo clean($i['full_name']); ?></td><td><?php echo clean($i['specialization']); ?></td><td><?php echo $i['availability_status']; ?></td><td><a class="btn btn-danger btn-sm" onclick="return confirm('Delete this therapist?')" href="?delete=<?php echo $i['therapist_id']; ?>">Delete</a></td></tr><?php endforeach; ?></tbody></table></div>
<?php include 'admin_footer.php'; ?>
