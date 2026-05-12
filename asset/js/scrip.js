
var MOCK_USERS = [
    { id: 1, fullName: 'Nguyễn Văn A', email: 'user@chonlibi.com',  password: 'User@123',  role: 'user'  },
    { id: 2, fullName: 'Quản Lý',      email: 'admin@chonlibi.com', password: 'Admin@123', role: 'admin' }
];

// mở đóng model
document.getElementById('openLoginBtn').addEventListener('click', function () {
    if (localStorage.getItem('fullName')) {
        window.location.href = 'user.html';
        return;
    }
    document.getElementById('login-modal').classList.add('active');
});

document.getElementById('closeLoginBtn').addEventListener('click', function () {
    document.getElementById('login-modal').classList.remove('active');
});

document.getElementById('login-modal').addEventListener('click', function (e) {
    if (e.target === this) this.classList.remove('active');
});

document.querySelectorAll('.modal-tab').forEach(function (tab) {
    tab.addEventListener('click', function () {
        document.querySelectorAll('.modal-tab').forEach(function (t) { t.classList.remove('active'); });
        document.querySelectorAll('.modal-panel').forEach(function (p) { p.classList.remove('active'); });
        tab.classList.add('active');
        document.getElementById('panel-' + tab.dataset.tab).classList.add('active');
    });
});

//login
document.getElementById('btn-login').addEventListener('click', function () {
    var email    = document.getElementById('login-email').value.trim();
    var password = document.getElementById('login-password').value;
    var errEl    = document.getElementById('login-error');
    errEl.textContent = '';

    if (!email || !password) { errEl.textContent = 'Vui lòng nhập email và mật khẩu.'; return; }

    // fetch('/api/login', { method: 'POST', headers: {'Content-Type':'application/json'},
    //         body: JSON.stringify({ email, password }) })
    //         .then(res => res.json()).then(data => { localStorage.setItem('token', data.token); ... })

    var user = MOCK_USERS.find(function (u) { return u.email === email && u.password === password; });
    if (!user) { errEl.textContent = 'Email hoặc mật khẩu không đúng.'; return; }

    localStorage.setItem('fullName', user.fullName);
    localStorage.setItem('email',    user.email);
    localStorage.setItem('role',     user.role);
    localStorage.setItem('userId',   user.id);

    document.getElementById('login-modal').classList.remove('active');
    updateHeaderAfterLogin(user.fullName);
});

//dki
document.getElementById('btn-register').addEventListener('click', function () {
    var fullName = document.getElementById('reg-fullname').value.trim();
    var phone    = document.getElementById('reg-phone').value.trim();
    var email    = document.getElementById('reg-email').value.trim();
    var password = document.getElementById('reg-password').value;
    var errEl    = document.getElementById('register-error');
    errEl.textContent = '';

    if (!fullName || !phone || !email || !password) { errEl.textContent = 'Vui lòng điền đầy đủ các trường.'; return; }
    if (password.length < 6) { errEl.textContent = 'Mật khẩu phải có ít nhất 6 ký tự.'; return; }

    // fetch('/api/register', { method: 'POST', ... })

    var exists = MOCK_USERS.find(function (u) { return u.email === email; });
    if (exists) { errEl.textContent = 'Email này đã được sử dụng.'; return; }

    MOCK_USERS.push({ id: MOCK_USERS.length + 1, fullName, phone, email, password, role: 'user' });
    alert('Đăng ký thành công! Vui lòng đăng nhập.');
    document.querySelector('[data-tab="login"]').click();
    document.getElementById('login-email').value = email;
});

function updateHeaderAfterLogin(name) {
    var btn = document.getElementById('openLoginBtn');
    var initial = name.charAt(0).toUpperCase();
    btn.innerHTML =
        '<span class="user-avatar-btn">' + initial + '</span>' +
        '<span id="login-btn-text">' + name.split(' ').pop() + '</span>';
}

(function checkLoginState() {
    var name = localStorage.getItem('fullName');
    if (!name) return;
    updateHeaderAfterLogin(name);
})();