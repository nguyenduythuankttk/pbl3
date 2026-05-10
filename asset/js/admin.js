// ── Bảo vệ trang 
(function guard() {
    var role = localStorage.getItem('role');
    var name = localStorage.getItem('fullName');
    if (!role || role !== 'admin') {
        alert('Bạn không có quyền truy cập trang này!');
        window.location.href = './index.html';
        return;
    }
    document.getElementById('adm-username').textContent = name || 'Admin';
})();

// ── Đăng xuất 
function adminLogout() {
    localStorage.clear();
    window.location.href = './index.html';
}

// ── Mock Data ───────────────────────────────────
var ORDERS = [
    { id: 1001, userName: 'Nguyễn Văn A', userId: 2, items: 'Đùi gà rán x2, Pepsi',  total: 150000, status: 'pending',    note: 'Không hành',     date: '2025-05-01' },
    { id: 1002, userName: 'Trần Thị B',   userId: 3, items: 'Combo gia đình',          total: 350000, status: 'confirmed',  note: 'Thêm tương ớt',  date: '2025-05-02' },
    { id: 1003, userName: 'Lê Văn C',     userId: 4, items: 'Cánh gà chiên mắm x3',   total: 120000, status: 'delivering', note: '',               date: '2025-05-03' },
    { id: 1004, userName: 'Nguyễn Văn A', userId: 2, items: 'Gà giòn sandwich',        total: 65000,  status: 'done',       note: '',               date: '2025-05-03' },
    { id: 1005, userName: 'Phạm Hương D', userId: 5, items: 'Bucket 9 miếng',          total: 250000, status: 'done',       note: 'Giao trước 12h', date: '2025-05-04' },
    { id: 1006, userName: 'Hoàng Minh E', userId: 6, items: 'Đùi gà x1, Khoai tây',   total: 95000,  status: 'cancelled',  note: 'Hết món',        date: '2025-05-04' },
];

var USERS = [
    { id: 1, fullName: 'Quản Lý',      email: 'admin@chonlibi.com', phone: '',             role: 'admin' },
    { id: 2, fullName: 'Nguyễn Văn A', email: 'user@chonlibi.com',  phone: '0912 345 678', role: 'user'  },
    { id: 3, fullName: 'Trần Thị B',   email: 'tran.b@gmail.com',   phone: '0987 654 321', role: 'user'  },
    { id: 4, fullName: 'Lê Văn C',     email: 'le.c@gmail.com',     phone: '0901 234 567', role: 'user'  },
    { id: 5, fullName: 'Phạm Hương D', email: 'pham.d@gmail.com',   phone: '0933 111 222', role: 'user'  },
    { id: 6, fullName: 'Hoàng Minh E', email: 'hoang.e@gmail.com',  phone: '0944 555 666', role: 'user'  },
];

var MENU_ITEMS = [
    { id: 1, name: 'Đùi gà rán giòn',       category: 'Gà rán',   price: 35000,  active: true  },
    { id: 2, name: 'Cánh gà chiên mắm',      category: 'Gà rán',   price: 32000,  active: true  },
    { id: 3, name: 'Combo 2 miếng + nước',   category: 'Combo',    price: 65000,  active: true  },
    { id: 4, name: 'Combo gia đình (9 miếng)',category: 'Combo',    price: 250000, active: true  },
    { id: 5, name: 'Gà giòn sandwich',        category: 'Sandwich', price: 45000,  active: true  },
    { id: 6, name: 'Khoai tây chiên',         category: 'Phụ',      price: 25000,  active: false },
];

var STATUS_LABEL = {
    pending: 'Chờ xác nhận', confirmed: 'Đã xác nhận',
    delivering: 'Đang giao', done: 'Hoàn thành', cancelled: 'Đã huỷ'
};

var currentOrderFilter = 'all';

function showSection(name) {
    document.querySelectorAll('.adm-section').forEach(function (s) { s.classList.remove('active'); });
    document.querySelectorAll('.sidebar-item').forEach(function (s) { s.classList.remove('active'); });
    document.getElementById('section-' + name).classList.add('active');

    var items = document.querySelectorAll('.sidebar-item');
    items.forEach(function (item) {
        if (item.getAttribute('onclick') && item.getAttribute('onclick').includes(name)) {
            item.classList.add('active');
        }
    });

    if (name === 'orders')    renderOrders();
    if (name === 'users')     renderUsers();
    if (name === 'menu')      renderMenuItems();
    if (name === 'dashboard') renderDashboard();
}

// ── DASHBOARD ──────────────────────────────────────────
function renderDashboard() {
    var done     = ORDERS.filter(function(o){ return o.status==='done'; });
    var revenue  = done.reduce(function(s,o){ return s+o.total; }, 0);

    document.getElementById('s-total').textContent     = ORDERS.length;
    document.getElementById('s-pending').textContent   = ORDERS.filter(function(o){ return o.status==='pending'; }).length;
    document.getElementById('s-delivering').textContent= ORDERS.filter(function(o){ return o.status==='delivering'; }).length;
    document.getElementById('s-done').textContent      = done.length;
    document.getElementById('s-revenue').textContent   = (revenue/1000).toFixed(0) + 'K';

    // 5 đơn gần nhất
    var recent = ORDERS.slice(0, 5);
    var tbody  = document.getElementById('dash-order-tbody');
    tbody.innerHTML = recent.map(function(o) {
        return '<tr>'
            + '<td style="font-weight:600;color:var(--primary)">#' + o.id + '</td>'
            + '<td>' + o.userName + '</td>'
            + '<td style="color:#555;font-size:12px;">' + o.items + '</td>'
            + '<td style="font-weight:600;">' + o.total.toLocaleString('vi-VN') + ' đ</td>'
            + '<td><span class="badge badge-' + o.status + '">' + STATUS_LABEL[o.status] + '</span></td>'
            + '</tr>';
    }).join('');
}

// ── ORDERS ────
function renderOrders() {
    var list = currentOrderFilter === 'all'
        ? ORDERS
        : ORDERS.filter(function(o){ return o.status === currentOrderFilter; });

    var tbody = document.getElementById('order-tbody');
    if (!list.length) {
        tbody.innerHTML = '<tr><td colspan="7" class="tbl-empty">Không có đơn hàng nào.</td></tr>';
        return;
    }
    tbody.innerHTML = list.map(function(o) {
        var init = o.userName.split(' ').map(function(w){ return w[0]; }).slice(-2).join('').toUpperCase();
        var opts = Object.keys(STATUS_LABEL).map(function(v) {
            return '<option value="' + v + '"' + (v===o.status?' selected':'') + '>' + STATUS_LABEL[v] + '</option>';
        }).join('');
        return '<tr>'
            + '<td style="font-weight:600;color:var(--primary)">#' + o.id + '</td>'
            + '<td><div class="user-cell"><div class="avatar">' + init + '</div>' + o.userName + '</div></td>'
            + '<td style="color:#555;font-size:12px;">' + o.items + '</td>'
            + '<td style="font-weight:600;">' + o.total.toLocaleString('vi-VN') + ' đ</td>'
            + '<td style="color:#888;font-size:12px;">' + o.date + '</td>'
            + '<td style="color:#888;font-size:12px;">' + (o.note || '—') + '</td>'
            + '<td><select class="status-sel" onchange="updateOrderStatus(' + o.id + ',this.value)">' + opts + '</select></td>'
            + '</tr>';
    }).join('');
}

function updateOrderStatus(id, status) {
    var o = ORDERS.find(function(o){ return o.id===id; });
    if (o) { o.status = status; renderDashboard(); }
}

// Filter tabs
document.getElementById('order-filter-tabs').addEventListener('click', function(e) {
    var btn = e.target.closest('.ftab');
    if (!btn) return;
    document.querySelectorAll('#order-filter-tabs .ftab').forEach(function(b){ b.classList.remove('active'); });
    btn.classList.add('active');
    currentOrderFilter = btn.dataset.filter;
    renderOrders();
});

// users
function renderUsers() {
    document.getElementById('user-count').textContent = USERS.length + ' tài khoản';
    var tbody = document.getElementById('user-tbody');
    tbody.innerHTML = USERS.map(function(u) {
        var orderCount = ORDERS.filter(function(o){ return o.userId===u.id; }).length;
        return '<tr>'
            + '<td style="color:#aaa;">' + u.id + '</td>'
            + '<td style="font-weight:600;">' + u.fullName + '</td>'
            + '<td style="color:#555;font-size:12px;">' + u.email + '</td>'
            + '<td style="color:#555;font-size:12px;">' + (u.phone || '—') + '</td>'
            + '<td><span class="badge badge-' + u.role + '">' + (u.role==='admin'?'Admin':'User') + '</span></td>'
            + '<td style="text-align:center;">' + orderCount + '</td>'
            + '</tr>';
    }).join('');
}

// menu
function renderMenuItems() {
    var tbody = document.getElementById('menu-tbody');
    tbody.innerHTML = MENU_ITEMS.map(function(m) {
        return '<tr>'
            + '<td style="color:#aaa;">' + m.id + '</td>'
            + '<td style="font-weight:600;">' + m.name + '</td>'
            + '<td style="color:#555;font-size:12px;">' + m.category + '</td>'
            + '<td style="font-weight:600;">' + m.price.toLocaleString('vi-VN') + ' đ</td>'
            + '<td><span class="badge ' + (m.active ? 'badge-done' : 'badge-cancelled') + '">'
            +    (m.active ? 'Còn hàng' : 'Hết hàng') + '</span></td>'
            + '</tr>';
    }).join('');
}

// ── Init ───────────────────────────────────────────────
renderDashboard();