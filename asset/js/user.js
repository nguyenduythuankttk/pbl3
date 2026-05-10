
// ── Bảo vệ trang ──────────────────────────────────────
(function guard() {
    var role = localStorage.getItem('role');
    var name = localStorage.getItem('fullName');
    if (!role || role !== 'user') {
        alert('Vui lòng đăng nhập với tài khoản khách hàng!');
        window.location.href = './index.html';
        return;
    }
    document.getElementById('header-name').textContent = name;
})();

function logout() { localStorage.clear(); window.location.href = './index.html'; }

// ── Mock data ──────────────────────────────────────────
var MENU = [
    { id: 1, name: 'Đùi gà rán giòn',        price: 35000,  emoji: '🍗' },
    { id: 2, name: 'Cánh gà chiên mắm',       price: 32000,  emoji: '🍖' },
    { id: 3, name: 'Combo 2 miếng + nước',    price: 65000,  emoji: '🥤' },
    { id: 4, name: 'Combo gia đình 9 miếng',  price: 250000, emoji: '🪣' },
    { id: 5, name: 'Gà giòn sandwich',         price: 45000,  emoji: '🥪' },
    { id: 6, name: 'Khoai tây chiên',          price: 25000,  emoji: '🍟' },
    { id: 7, name: 'Pepsi lon',                price: 15000,  emoji: '🥤' },
    { id: 8, name: 'Nước cam ép',              price: 20000,  emoji: '🍊' },
];

var ORDERS = JSON.parse(localStorage.getItem('user_orders') || '[]');
var CART   = {};

var STATUS_LABEL = {
    pending:    '<span class="px-2 py-0.5 rounded-full text-xs font-bold bg-yellow-100 text-yellow-700">Chờ xác nhận</span>',
    confirmed:  '<span class="px-2 py-0.5 rounded-full text-xs font-bold bg-blue-100 text-blue-700">Đã xác nhận</span>',
    delivering: '<span class="px-2 py-0.5 rounded-full text-xs font-bold bg-emerald-100 text-emerald-700">Đang giao</span>',
    done:       '<span class="px-2 py-0.5 rounded-full text-xs font-bold bg-green-100 text-green-800">Hoàn thành</span>',
    cancelled:  '<span class="px-2 py-0.5 rounded-full text-xs font-bold bg-red-100 text-red-700">Đã huỷ</span>',
};

// ── Navigation ─────────────────────────────────────────
function showTab(name) {
    document.querySelectorAll('[id^="section-"]').forEach(function(s) { s.classList.add('hidden'); });
    document.querySelectorAll('.tab-item').forEach(function(t) {
        t.classList.remove('bg-orange-50', 'text-[rgb(220,77,11)]', 'border-[rgb(220,77,11)]');
        t.classList.add('text-gray-500', 'border-transparent');
    });
    document.getElementById('section-' + name).classList.remove('hidden');
    var active = document.getElementById('tab-' + name);
    active.classList.add('bg-orange-50', 'text-[rgb(220,77,11)]', 'border-[rgb(220,77,11)]');
    active.classList.remove('text-gray-500', 'border-transparent');

    if (name === 'order')     renderMenu();
    if (name === 'my-orders') renderMyOrders();
    if (name === 'history')   renderHistory();
}

// ── Render menu ────────────────────────────────────────
function renderMenu() {
    var grid = document.getElementById('menu-grid');
    grid.innerHTML = MENU.map(function(m) {
        return '<div class="bg-white border border-gray-200 rounded-xl p-4 flex flex-col items-center gap-2 hover:shadow-md transition">'
            + '<div class="text-4xl">' + m.emoji + '</div>'
            + '<div class="font-bold text-sm text-center">' + m.name + '</div>'
            + '<div class="text-[rgb(220,77,11)] font-bold text-sm">' + m.price.toLocaleString('vi-VN') + ' đ</div>'
            + '<div class="flex items-center gap-2 mt-1">'
            +   '<button onclick="changeQty(' + m.id + ',-1)" class="w-7 h-7 rounded-full border border-gray-300 text-gray-600 hover:border-[rgb(220,77,11)] hover:text-[rgb(220,77,11)] font-bold transition">−</button>'
            +   '<span id="qty-' + m.id + '" class="text-sm font-bold w-4 text-center">' + (CART[m.id] || 0) + '</span>'
            +   '<button onclick="changeQty(' + m.id + ',1)" class="w-7 h-7 rounded-full border border-gray-300 text-gray-600 hover:border-[rgb(220,77,11)] hover:text-[rgb(220,77,11)] font-bold transition">+</button>'
            + '</div>'
            + '</div>';
    }).join('');
}

// ── Giỏ hàng ───────────────────────────────────────────
function changeQty(id, delta) {
    CART[id] = Math.max(0, (CART[id] || 0) + delta);
    if (CART[id] === 0) delete CART[id];
    var el = document.getElementById('qty-' + id);
    if (el) el.textContent = CART[id] || 0;
    renderCart();
}

function renderCart() {
    var list  = document.getElementById('cart-list');
    var keys  = Object.keys(CART);
    if (!keys.length) {
        list.innerHTML = '<p class="text-gray-400 text-sm text-center py-4">Chưa có món nào</p>';
        document.getElementById('cart-total').textContent = '0 đ';
        return;
    }
    var total = 0;
    list.innerHTML = keys.map(function(id) {
        var item  = MENU.find(function(m){ return m.id == id; });
        var sub   = item.price * CART[id];
        total    += sub;
        return '<div class="flex justify-between items-center py-1.5 border-b border-gray-50 last:border-0 text-sm">'
            + '<span>' + item.emoji + ' ' + item.name + ' × ' + CART[id] + '</span>'
            + '<span class="font-bold text-[rgb(220,77,11)]">' + sub.toLocaleString('vi-VN') + ' đ</span>'
            + '</div>';
    }).join('');
    document.getElementById('cart-total').textContent = total.toLocaleString('vi-VN') + ' đ';
}

function clearCart() {
    CART = {};
    renderMenu();
    renderCart();
}

// ── Đặt hàng ───────────────────────────────────────────
function placeOrder() {
    if (!Object.keys(CART).length) { alert('Vui lòng chọn món!'); return; }
    var items = Object.keys(CART).map(function(id) {
        var item = MENU.find(function(m){ return m.id == id; });
        return item.name + ' ×' + CART[id];
    }).join(', ');
    var total = Object.keys(CART).reduce(function(s, id) {
        return s + MENU.find(function(m){ return m.id == id; }).price * CART[id];
    }, 0);

    var order = {
        id:     1000 + ORDERS.length + 1,
        userId: localStorage.getItem('userId'),
        items:  items,
        total:  total,
        note:   document.getElementById('order-note').value,
        status: 'pending',
        date:   new Date().toLocaleDateString('vi-VN')
    };
    ORDERS.push(order);
    localStorage.setItem('user_orders', JSON.stringify(ORDERS));

    CART = {};
    document.getElementById('order-note').value = '';
    renderMenu();
    renderCart();
    alert('✅ Đặt hàng thành công! Mã đơn: #' + order.id);
    showTab('my-orders');
}

// ── Đơn của tôi ────────────────────────────────────────
function renderMyOrders() {
    var active = ORDERS.filter(function(o){ return o.status !== 'done' && o.status !== 'cancelled'; });
    var tbody  = document.getElementById('my-orders-tbody');
    if (!active.length) {
        tbody.innerHTML = '<tr><td colspan="6" class="text-center py-10 text-gray-400">Không có đơn hàng đang xử lý</td></tr>';
        return;
    }
    tbody.innerHTML = active.map(function(o) {
        return '<tr class="border-b border-gray-50 hover:bg-orange-50/30 transition">'
            + '<td class="px-4 py-3 font-bold text-[rgb(220,77,11)]">#' + o.id + '</td>'
            + '<td class="px-4 py-3 text-gray-600 text-xs">' + o.items + '</td>'
            + '<td class="px-4 py-3 font-bold">' + Number(o.total).toLocaleString('vi-VN') + ' đ</td>'
            + '<td class="px-4 py-3 text-gray-400 text-xs">' + (o.note || '—') + '</td>'
            + '<td class="px-4 py-3 text-gray-400 text-xs">' + o.date + '</td>'
            + '<td class="px-4 py-3">' + (STATUS_LABEL[o.status] || o.status) + '</td>'
            + '</tr>';
    }).join('');
}

// ── Lịch sử ────────────────────────────────────────────
function renderHistory() {
    var done  = ORDERS.filter(function(o){ return o.status === 'done' || o.status === 'cancelled'; });
    var tbody = document.getElementById('history-tbody');
    if (!done.length) {
        tbody.innerHTML = '<tr><td colspan="5" class="text-center py-10 text-gray-400">Chưa có lịch sử đơn hàng</td></tr>';
        return;
    }
    tbody.innerHTML = done.map(function(o) {
        return '<tr class="border-b border-gray-50 hover:bg-orange-50/30 transition">'
            + '<td class="px-4 py-3 font-bold text-[rgb(220,77,11)]">#' + o.id + '</td>'
            + '<td class="px-4 py-3 text-gray-600 text-xs">' + o.items + '</td>'
            + '<td class="px-4 py-3 font-bold">' + Number(o.total).toLocaleString('vi-VN') + ' đ</td>'
            + '<td class="px-4 py-3 text-gray-400 text-xs">' + o.date + '</td>'
            + '<td class="px-4 py-3">' + (STATUS_LABEL[o.status] || o.status) + '</td>'
            + '</tr>';
    }).join('');
}

// ── Init ───────────────────────────────────────────────
showTab('order');   