// // =============================================
// // MOCK DATA
// // =============================================
// var MOCK_USERS = [
//     { id: 1, fullName: 'Quản Lý',       email: 'admin@chonlibi.com',    password: 'Admin@123',    role: 'admin'    },
//     { id: 2, fullName: 'Nhân Viên A',   email: 'employee@chonlibi.com', password: 'Employee@123', role: 'employee' },
//     { id: 3, fullName: 'Nguyễn Văn A',  email: 'user@chonlibi.com',     password: 'User@123',     role: 'user'     }
// ];

// // =============================================
// // MODAL: Mở / Đóng
// // =============================================
// var openBtn  = document.getElementById('openLoginBtn');
// var modal    = document.getElementById('login-modal');
// var closeBtn = document.getElementById('closeLoginBtn');

// openBtn.onclick = function () {
//     if (localStorage.getItem('role')) { toggleDropdown(); return; }
//     modal.classList.add('active');
// };
// closeBtn.onclick = function () { modal.classList.remove('active'); };
// modal.onclick = function (e) {
//     if (e.target === modal) modal.classList.remove('active');
// };

// // =============================================
// // TABS: Đăng Nhập / Đăng Ký
// // =============================================
// document.querySelectorAll('.modal-tab').forEach(function (tab) {
//     tab.onclick = function () {
//         document.querySelectorAll('.modal-tab').forEach(function (t) { t.classList.remove('active'); });
//         document.querySelectorAll('.modal-panel').forEach(function (p) { p.classList.remove('active'); });
//         tab.classList.add('active');
//         document.getElementById('panel-' + tab.dataset.tab).classList.add('active');
//     };
// });

// // =============================================
// // ĐĂNG NHẬP
// // =============================================
// document.getElementById('btn-login').onclick = function () {
//     var email    = document.getElementById('login-email').value.trim();
//     var password = document.getElementById('login-password').value;
//     var errEl    = document.getElementById('login-error');
//     errEl.textContent = '';

//     if (!email || !password) { errEl.textContent = 'Vui lòng nhập email và mật khẩu.'; return; }

//     var user = MOCK_USERS.find(function (u) { return u.email === email && u.password === password; });
//     if (!user) { errEl.textContent = 'Email hoặc mật khẩu không đúng.'; return; }

//     localStorage.setItem('fullName', user.fullName);
//     localStorage.setItem('email',    user.email);
//     localStorage.setItem('role',     user.role);
//     localStorage.setItem('userId',   user.id);

//     modal.classList.remove('active');

//     // Phân quyền → chuyển trang
//     if (user.role === 'admin')    { window.location.href = './admin.html';    return; }
//     if (user.role === 'employee') { window.location.href = './employee.html'; return; }
//     if (user.role === 'user')     { window.location.href = './user.html';     return; }
// };

// // =============================================
// // ĐĂNG KÝ
// // =============================================
// document.getElementById('btn-register').onclick = function () {
//     var fullName = document.getElementById('reg-fullname').value.trim();
//     var phone    = document.getElementById('reg-phone').value.trim();
//     var email    = document.getElementById('reg-email').value.trim();
//     var password = document.getElementById('reg-password').value;
//     var errEl    = document.getElementById('register-error');
//     errEl.textContent = '';

//     if (!fullName || !email || !password) { errEl.textContent = 'Vui lòng điền đầy đủ thông tin.'; return; }
//     if (password.length < 6) { errEl.textContent = 'Mật khẩu tối thiểu 6 ký tự.'; return; }

//     var exists = MOCK_USERS.find(function (u) { return u.email === email; });
//     if (exists) { errEl.textContent = 'Email này đã được sử dụng.'; return; }

//     MOCK_USERS.push({ id: MOCK_USERS.length + 1, fullName: fullName, phone: phone, email: email, password: password, role: 'user' });
//     alert('Đăng ký thành công! Vui lòng đăng nhập.');
//     document.querySelector('[data-tab="login"]').click();
//     document.getElementById('login-email').value = email;
// };

// // =============================================
// // DROPDOWN
// // =============================================
// function toggleDropdown() {
//     document.getElementById('user-dropdown').classList.toggle('active');
// }
// document.addEventListener('click', function (e) {
//     var dd  = document.getElementById('user-dropdown');
//     var btn = document.getElementById('openLoginBtn');
//     if (dd && btn && !dd.contains(e.target) && !btn.contains(e.target)) {
//         dd.classList.remove('active');
//     }
// });

// // =============================================
// // ĐĂNG XUẤT
// // =============================================
// document.getElementById('btn-logout').onclick = function (e) {
//     e.preventDefault();
//     localStorage.clear();
//     window.location.reload();
// };

// // =============================================
// // CẬP NHẬT HEADER
// // =============================================
// function updateHeaderLoggedIn(name) {
//     var btnText = document.getElementById('login-btn-text');
//     if (btnText) btnText.textContent = name;
//     var ddName  = document.getElementById('dropdown-name');
//     var ddEmail = document.getElementById('dropdown-email');
//     if (ddName)  ddName.textContent  = name;
//     if (ddEmail) ddEmail.textContent = localStorage.getItem('email') || '';
// }

// // =============================================
// // KIỂM TRA TRẠNG THÁI KHI TẢI TRANG
// // =============================================
// (function checkLoginState() {
//     var name = localStorage.getItem('fullName');
//     var role = localStorage.getItem('role');
//     if (!name) return;

//     if (role === 'admin'    && !window.location.pathname.includes('admin.html'))    { window.location.href = './admin.html';    return; }
//     if (role === 'employee' && !window.location.pathname.includes('employee.html')) { window.location.href = './employee.html'; return; }
//     if (role === 'user'     && !window.location.pathname.includes('user.html'))     { window.location.href = './user.html';     return; }

//     updateHeaderLoggedIn(name);
// })();