<?php include 'header.php';
$message = '';
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $email = clean($_POST['email']);
    $password = $_POST['password'];
    $stmt = $pdo->prepare("SELECT * FROM users WHERE email=?");
    $stmt->execute([$email]);
    $user = $stmt->fetch(PDO::FETCH_ASSOC);
    if ($user && password_verify($password, $user['password'])) {
        $_SESSION['user_id'] = $user['user_id'];
        $_SESSION['full_name'] = $user['full_name'];
        $_SESSION['role'] = $user['role'];
        redirect($user['role'] === 'admin' ? 'admin/dashboard.php' : 'index.php');
    } else {
        $message = '<div class="alert alert-danger">Invalid email or password.</div>';
    }
}
?>
<div class="container my-5"><div class="row justify-content-center"><div class="col-md-5">
<div class="card p-4"><h3>Login</h3><?php echo $message; ?>
<form method="post">
  <input class="form-control mb-3" type="email" name="email" placeholder="Email" required>
  <input class="form-control mb-3" type="password" name="password" placeholder="Password" required>
  <button class="btn btn-main w-100">Login</button>
</form>
<p class="mt-3 small">Admin: admin@example.com / admin123</p>
</div></div></div></div>
<?php include 'footer.php'; ?>
