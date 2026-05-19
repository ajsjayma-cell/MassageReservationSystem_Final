<?php

include 'config.php';
include 'functions.php';

$message = '';

if(isset($_POST['login']))
{
    $email = clean($_POST['email']);
    $password = $_POST['password'];

    $stmt = $pdo->prepare(
        "SELECT * FROM users WHERE email=? LIMIT 1"
    );

    $stmt->execute([$email]);

    $user = $stmt->fetch(PDO::FETCH_ASSOC);

    if($user)
    {
        $valid = false;

        // PHP PASSWORD HASH
        if(password_verify($password, $user['password']))
        {
            $valid = true;
        }

        // SHA256 SUPPORT
        else if(
            hash('sha256', $password)
            === strtolower($user['password'])
        )
        {
            $valid = true;
        }

        if($valid)
        {
            $_SESSION['user_id'] =
                $user['user_id'];

            $_SESSION['full_name'] =
                $user['full_name'];

            $_SESSION['role'] =
                $user['role'];

            if($user['role'] == 'admin')
            {
                header(
                    "Location: admin/dashboard.php"
                );
            }
            else
            {
                header(
                    "Location: index.php"
                );
            }

            exit;
        }
    }

    $message =
        '<div class="alert alert-danger">
            Invalid email or password.
        </div>';
}
?>

<!DOCTYPE html>

<html>

<head>

    <title>Login</title>

    <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet"
    >

</head>

<body class="bg-light">

<div class="container py-5">

    <div class="row justify-content-center">

        <div class="col-md-5">

            <div class="card shadow border-0 p-4">

                <h2 class="text-center mb-4">
                    Login
                </h2>

                <?php echo $message; ?>

                <form method="POST">

                    <input
                        type="email"
                        name="email"
                        class="form-control mb-3"
                        placeholder="Email"
                        required
                    >

                    <input
                        type="password"
                        name="password"
                        class="form-control mb-3"
                        placeholder="Password"
                        required
                    >

                    <button
                        type="submit"
                        name="login"
                        class="btn btn-dark w-100"
                    >
                        Login
                    </button>

                </form>

            </div>

        </div>

    </div>

</div>

</body>

</html>