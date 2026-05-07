<?php include 'admin_header.php';
if (isset($_POST['add'])) {
    $stmt = $pdo->prepare("INSERT INTO services(service_name,description,price,duration) VALUES(?,?,?,?)");
    $stmt->execute([clean($_POST['service_name']), clean($_POST['description']), $_POST['price'], $_POST['duration']]);
}
if (isset($_GET['delete'])) {
    $stmt = $pdo->prepare("DELETE FROM services WHERE service_id=?");
    $stmt->execute([(int)$_GET['delete']]);
}
$items = $pdo->query("SELECT * FROM services ORDER BY service_name")->fetchAll(PDO::FETCH_ASSOC);
?>
<div class="container my-5"><h2>Manage Services</h2><div class="card p-4 mb-4"><form method="post" class="row g-3"><div class="col-md-3"><input class="form-control" name="service_name" placeholder="Service Name" required></div><div class="col-md-3"><input class="form-control" name="description" placeholder="Description"></div><div class="col-md-2"><input class="form-control" type="number" step="0.01" name="price" placeholder="Price" required></div><div class="col-md-2"><input class="form-control" type="number" name="duration" placeholder="Duration" required></div><div class="col-md-2"><button name="add" class="btn btn-main w-100">Add</button></div></form></div><table class="table table-bordered"><thead><tr><th>Name</th><th>Description</th><th>Price</th><th>Duration</th><th></th></tr></thead><tbody><?php foreach($items as $i): ?><tr><td><?php echo clean($i['service_name']); ?></td><td><?php echo clean($i['description']); ?></td><td>₱<?php echo number_format($i['price'],2); ?></td><td><?php echo $i['duration']; ?> mins</td><td><a class="btn btn-danger btn-sm" onclick="return confirm('Delete this service?')" href="?delete=<?php echo $i['service_id']; ?>">Delete</a></td></tr><?php endforeach; ?></tbody></table></div>
<?php include 'admin_footer.php'; ?>
