<?php include 'header.php';
$message = '';
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $name = clean($_POST['full_name']);
    $email = clean($_POST['email']);
    $pass = password_hash($_POST['password'], PASSWORD_DEFAULT);
    try {
        $stmt = $pdo->prepare("INSERT INTO users(full_name,email,password) VALUES(?,?,?)");
        $stmt->execute([$name,$email,$pass]);
        $message = '<div class="alert alert-success">Registration successful. You can now login.</div>';
    } catch (PDOException $e) {
        $message = '<div class="alert alert-danger">Email already exists.</div>';
    }
}
?>
<div class="container my-5"><div class="row justify-content-center"><div class="col-md-5">
<div class="card p-4"><h3>Register</h3><?php echo $message; ?>
<form method="post">
  <input class="form-control mb-3" name="full_name" placeholder="Full Name" required>
  <input class="form-control mb-3" type="email" name="email" placeholder="Email" required>
  <input class="form-control mb-3" type="password" name="password" placeholder="Password" required>
  <button class="btn btn-main w-100">Register</button>
</form></div></div></div></div>
<?php include 'footer.php'; ?>
