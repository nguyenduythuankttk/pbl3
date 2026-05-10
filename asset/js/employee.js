// ── Bảo vệ trang ──────────────────────────────────────
(function guard() {
    var role = localStorage.getItem('role');
    var name = localStorage.getItem('fullName');
    if (!role || role !== 'employee') {
        alert('Vui lòng đăng nhập với tài khoản nhân viên!');
        window.location.href = './index.html';
        return;
    }
    document.getElementById('header-name').textContent = name;
})();

function logout() { localStorage.clear(); window.location.href = './index.html'; }

// ── Mock data ──────────────────────────────────────────
var MENU = [
    { id: 1, name: 'Đùi gà rán giòn',       price: 35000  },
    { id: 2, name: 'Cánh gà chiên mắm',      price: 32000  },
    { id: 3, name: 'Combo 2 miếng + nước',   price: 65000  },
    { id: 4, name: 'Combo gia đình 9 miếng', price: 250000 },
    { id: 5, name: 'Gà giòn sandwich',        price: 45000  },
    { id: 6, name: 'Khoai tây chiên',         price: 25000  },
    { id: 7, name: 'Pepsi lon',               price: 15000  },
];

var INVOICES      = [];
var INV_CART      = {};
var DELIVERIES    = [
    { id: 2001, customer: 'Trần Thị B',   address: '12 Lê Lợi, Q1',       status: 'pending'  },
    { id: 2002, customer: 'Lê Văn C',     address: '45 Nguyễn Huệ, Q3',   status: 'pending'  },
    { id: 2003, customer: 'Phạm Hương D', address: '78 Hai Bà Trưng, Q5', status: 'delivered', deliveredAt: '10:30', deliveredBy: 'Nhân Viên A' },
];
var WAREHOUSE_LOG = [];
var PURCHASES     = [
    { id: 'PO-001', items: 'Gà tươi 50kg',    supplier: 'Cty Gà Sạch',   value: 2500000, status: 'pending', log: [] },
    { id: 'PO-002', items: 'Dầu ăn 20L',       supplier: 'Cty Thực Phẩm', value: 800000,  status: 'pending', log: [] },
    { id: 'PO-003', items: 'Bột chiên giòn 10kg', supplier: 'Cty Gia Vị', value: 450000,  status: 'approved', log: ['Đã duyệt lúc 08:00'] },
];
var BOOKINGS      = [];
var SHIFTS        = [];

var TYPE_LABEL  = { takeaway: 'Mang về', 'dine-in': 'Tại quán', delivery: 'Giao hàng' };
var SHIFT_LABEL = { morning: 'Ca sáng (06–14h)', afternoon: 'Ca chiều (14–22h)', night: 'Ca đêm (22–06h)' };

// ── Navigation ─────────────────────────────────────────
function showTab(name) {
    document.querySelectorAll('[id^="section-"]').forEach(function(s) { s.classList.add('hidden'); });
    document.querySelectorAll('.tab-item').forEach(function(t) {
        t.classList.remove('bg-orange-50', 'text-[rgb(220,77,11)]', 'border-[rgb(220,77,11)]');
        t.classList.add('text-gray-500', 'border-transparent');
    });
    document.getElementById('section-' + name).classList.remove('hidden');
    var active = document.getElementById('tab-' + name);
    if (active) {
        active.classList.add('bg-orange-50', 'text-[rgb(220,77,11)]', 'border-[rgb(220,77,11)]');
        active.classList.remove('text-gray-500', 'border-transparent');
    }
    var renders = {
        invoice:   function(){ renderInvMenu(); renderInvoices(); },
        delivery:  renderDelivery,
        warehouse: renderWarehouse,
        purchase:  renderPurchase,
        booking:   renderBookings,
        shift:     renderShifts
    };
    if (renders[name]) renders[name]();
}

// ── LẬP HÓA ĐƠN ───────────────────────────────────────
function renderInvMenu() {
    document.getElementById('inv-menu').innerHTML = MENU.map(function(m) {
        return '<div class="flex items-center justify-between py-1.5">'
            + '<span class="text-sm">' + m.name + ' <span class="text-[rgb(220,77,11)] font-bold">' + m.price.toLocaleString('vi-VN') + 'đ</span></span>'
            + '<div class="flex items-center gap-2">'
            + '<button onclick="invQty(' + m.id + ',-1)" class="w-6 h-6 rounded-full border border-gray-300 text-xs font-bold hover:border-[rgb(220,77,11)] hover:text-[rgb(220,77,11)]">−</button>'
            + '<span id="inv-qty-' + m.id + '" class="text-sm font-bold w-4 text-center">' + (INV_CART[m.id] || 0) + '</span>'
            + '<button onclick="invQty(' + m.id + ',1)" class="w-6 h-6 rounded-full border border-gray-300 text-xs font-bold hover:border-[rgb(220,77,11)] hover:text-[rgb(220,77,11)]">+</button>'
            + '</div></div>';
    }).join('');
    updateInvTotal();
}

function invQty(id, delta) {
    INV_CART[id] = Math.max(0, (INV_CART[id] || 0) + delta);
    if (!INV_CART[id]) delete INV_CART[id];
    var el = document.getElementById('inv-qty-' + id);
    if (el) el.textContent = INV_CART[id] || 0;
    updateInvTotal();
}

function updateInvTotal() {
    var total = Object.keys(INV_CART).reduce(function(s, id) {
        return s + MENU.find(function(m){ return m.id == id; }).price * INV_CART[id];
    }, 0);
    document.getElementById('inv-total').textContent = total.toLocaleString('vi-VN') + ' đ';
}

function createInvoice() {
    var customer = document.getElementById('inv-customer').value.trim();
    var phone    = document.getElementById('inv-phone').value.trim();
    var type     = document.getElementById('inv-type').value;
    if (!customer) { alert('Vui lòng nhập tên khách hàng!'); return; }
    if (!Object.keys(INV_CART).length) { alert('Vui lòng chọn món!'); return; }

    var items = Object.keys(INV_CART).map(function(id) {
        return MENU.find(function(m){ return m.id == id; }).name + ' ×' + INV_CART[id];
    }).join(', ');
    var total = Object.keys(INV_CART).reduce(function(s, id) {
        return s + MENU.find(function(m){ return m.id == id; }).price * INV_CART[id];
    }, 0);

    INVOICES.unshift({ id: 'HD-' + (INVOICES.length + 1), customer: customer, phone: phone, items: items, total: total, type: type, time: new Date().toLocaleTimeString('vi-VN', {hour:'2-digit',minute:'2-digit'}) });
    INV_CART = {};
    document.getElementById('inv-customer').value = '';
    document.getElementById('inv-phone').value    = '';
    renderInvMenu();
    renderInvoices();
    alert('✅ Xuất hóa đơn thành công!');
}

function renderInvoices() {
    var tbody = document.getElementById('invoice-tbody');
    if (!INVOICES.length) { tbody.innerHTML = '<tr><td colspan="5" class="text-center py-8 text-gray-400 text-xs">Chưa có hóa đơn</td></tr>'; return; }
    tbody.innerHTML = INVOICES.map(function(inv) {
        return '<tr class="border-b border-gray-50 hover:bg-orange-50/30 text-sm">'
            + '<td class="px-4 py-2.5 font-bold text-[rgb(220,77,11)]">' + inv.id + '</td>'
            + '<td class="px-4 py-2.5">' + inv.customer + '</td>'
            + '<td class="px-4 py-2.5 font-bold">' + inv.total.toLocaleString('vi-VN') + 'đ</td>'
            + '<td class="px-4 py-2.5"><span class="px-2 py-0.5 rounded-full text-xs font-bold bg-blue-100 text-blue-700">' + TYPE_LABEL[inv.type] + '</span></td>'
            + '<td class="px-4 py-2.5 text-gray-400 text-xs">' + inv.time + '</td>'
            + '</tr>';
    }).join('');
}

// ── GIAO HÀNG ──────────────────────────────────────────
function renderDelivery() {
    var pending = DELIVERIES.filter(function(d){ return d.status === 'pending'; });
    var done    = DELIVERIES.filter(function(d){ return d.status === 'delivered'; });

    var pt = document.getElementById('delivery-pending-tbody');
    pt.innerHTML = pending.length ? pending.map(function(d) {
        return '<tr class="border-b border-gray-50 text-sm">'
            + '<td class="px-4 py-2.5 font-bold text-[rgb(220,77,11)]">#' + d.id + '</td>'
            + '<td class="px-4 py-2.5">' + d.customer + '</td>'
            + '<td class="px-4 py-2.5 text-gray-500 text-xs">' + d.address + '</td>'
            + '<td class="px-4 py-2.5"><button onclick="confirmDelivery(' + d.id + ')" class="bg-green-100 text-green-700 hover:bg-green-200 text-xs font-bold px-3 py-1.5 rounded-lg transition">✓ Đã giao</button></td>'
            + '</tr>';
    }).join('') : '<tr><td colspan="4" class="text-center py-8 text-gray-400 text-xs">Không có đơn cần giao</td></tr>';

    var dt = document.getElementById('delivery-done-tbody');
    dt.innerHTML = done.length ? done.map(function(d) {
        return '<tr class="border-b border-gray-50 text-sm">'
            + '<td class="px-4 py-2.5 font-bold text-[rgb(220,77,11)]">#' + d.id + '</td>'
            + '<td class="px-4 py-2.5">' + d.customer + '</td>'
            + '<td class="px-4 py-2.5 text-gray-400 text-xs">' + (d.deliveredAt || '—') + '</td>'
            + '<td class="px-4 py-2.5 text-gray-500 text-xs">' + (d.deliveredBy || '—') + '</td>'
            + '</tr>';
    }).join('') : '<tr><td colspan="4" class="text-center py-8 text-gray-400 text-xs">Chưa có lịch sử</td></tr>';
}

function confirmDelivery(id) {
    var d = DELIVERIES.find(function(d){ return d.id === id; });
    if (!d) return;
    d.status      = 'delivered';
    d.deliveredAt = new Date().toLocaleTimeString('vi-VN', {hour:'2-digit',minute:'2-digit'});
    d.deliveredBy = localStorage.getItem('fullName');
    renderDelivery();
}

// ── NHẬP KHO ───────────────────────────────────────────
function confirmWarehouse() {
    var supplier = document.getElementById('wh-supplier').value.trim();
    var items    = document.getElementById('wh-items').value.trim();
    var receiver = document.getElementById('wh-receiver').value.trim();
    var note     = document.getElementById('wh-note').value.trim();
    if (!supplier || !items || !receiver) { alert('Vui lòng điền đầy đủ thông tin!'); return; }

    WAREHOUSE_LOG.unshift({
        supplier: supplier, items: items, receiver: receiver, note: note,
        time: new Date().toLocaleString('vi-VN')
    });
    document.getElementById('wh-supplier').value = '';
    document.getElementById('wh-items').value    = '';
    document.getElementById('wh-receiver').value = '';
    document.getElementById('wh-note').value     = '';
    renderWarehouse();
    alert('✅ Xác nhận nhận hàng thành công!');
}

function renderWarehouse() {
    var tbody = document.getElementById('warehouse-tbody');
    if (!WAREHOUSE_LOG.length) { tbody.innerHTML = '<tr><td colspan="4" class="text-center py-8 text-gray-400 text-xs">Chưa có dữ liệu</td></tr>'; return; }
    tbody.innerHTML = WAREHOUSE_LOG.map(function(w) {
        return '<tr class="border-b border-gray-50 hover:bg-orange-50/30 text-sm">'
            + '<td class="px-4 py-2.5 font-bold">' + w.supplier + '</td>'
            + '<td class="px-4 py-2.5 text-gray-600 text-xs">' + w.items + '</td>'
            + '<td class="px-4 py-2.5">' + w.receiver + '</td>'
            + '<td class="px-4 py-2.5 text-gray-400 text-xs">' + w.time + '</td>'
            + '</tr>';
    }).join('');
}

// ── ĐƠN MUA HÀNG ──────────────────────────────────────
function renderPurchase() {
    var tbody = document.getElementById('purchase-tbody');
    tbody.innerHTML = PURCHASES.map(function(p) {
        var badgeClass = p.status === 'approved' ? 'bg-green-100 text-green-700' : p.status === 'rejected' ? 'bg-red-100 text-red-700' : 'bg-yellow-100 text-yellow-700';
        var badgeText  = p.status === 'approved' ? 'Đã duyệt' : p.status === 'rejected' ? 'Từ chối' : 'Chờ duyệt';
        var btns = p.status === 'pending'
            ? '<button onclick="approvePO(\'' + p.id + '\',\'approved\')" class="bg-green-100 text-green-700 hover:bg-green-200 text-xs font-bold px-2.5 py-1 rounded-lg mr-1 transition">✓ Duyệt</button>'
            + '<button onclick="approvePO(\'' + p.id + '\',\'rejected\')" class="bg-red-100 text-red-700 hover:bg-red-200 text-xs font-bold px-2.5 py-1 rounded-lg transition">✕ Từ chối</button>'
            : '<span class="text-gray-300 text-xs">—</span>';
        return '<tr class="border-b border-gray-50 hover:bg-orange-50/30 text-sm">'
            + '<td class="px-4 py-3 font-bold text-[rgb(220,77,11)]">' + p.id + '</td>'
            + '<td class="px-4 py-3">' + p.items + '</td>'
            + '<td class="px-4 py-3 text-gray-500 text-xs">' + p.supplier + '</td>'
            + '<td class="px-4 py-3 font-bold">' + p.value.toLocaleString('vi-VN') + 'đ</td>'
            + '<td class="px-4 py-3"><span class="px-2 py-0.5 rounded-full text-xs font-bold ' + badgeClass + '">' + badgeText + '</span></td>'
            + '<td class="px-4 py-3 text-gray-400 text-xs">' + (p.log.slice(-1)[0] || '—') + '</td>'
            + '<td class="px-4 py-3">' + btns + '</td>'
            + '</tr>';
    }).join('');
    renderPurchaseLog();
}

function approvePO(id, action) {
    var p = PURCHASES.find(function(p){ return p.id === id; });
    if (!p) return;
    p.status = action;
    var msg = (action === 'approved' ? 'Đã duyệt' : 'Đã từ chối') + ' lúc ' + new Date().toLocaleTimeString('vi-VN', {hour:'2-digit',minute:'2-digit'}) + ' bởi ' + localStorage.getItem('fullName');
    p.log.push(msg);
    renderPurchase();
}

function renderPurchaseLog() {
    var allLogs = [];
    PURCHASES.forEach(function(p) {
        p.log.forEach(function(l) { allLogs.push({ id: p.id, log: l }); });
    });
    var tbody = document.getElementById('purchase-log-tbody');
    if (!allLogs.length) { tbody.innerHTML = '<tr><td colspan="4" class="text-center py-8 text-gray-400 text-xs">Chưa có lịch sử</td></tr>'; return; }
    tbody.innerHTML = allLogs.map(function(l) {
        return '<tr class="border-b border-gray-50 text-sm">'
            + '<td class="px-4 py-2.5 font-bold text-[rgb(220,77,11)]">' + l.id + '</td>'
            + '<td class="px-4 py-2.5 text-gray-600">' + l.log + '</td>'
            + '<td class="px-4 py-2.5 text-gray-400 text-xs">' + localStorage.getItem('fullName') + '</td>'
            + '<td class="px-4 py-2.5 text-gray-400 text-xs">' + new Date().toLocaleDateString('vi-VN') + '</td>'
            + '</tr>';
    }).join('');
}

// ── ĐẶT BÀN ───────────────────────────────────────────
function addBooking() {
    var name   = document.getElementById('bk-name').value.trim();
    var phone  = document.getElementById('bk-phone').value.trim();
    var date   = document.getElementById('bk-date').value;
    var time   = document.getElementById('bk-time').value;
    var guests = document.getElementById('bk-guests').value;
    var table  = document.getElementById('bk-table').value;
    if (!name || !date || !time || !guests) { alert('Vui lòng điền đầy đủ thông tin!'); return; }

    BOOKINGS.unshift({ name: name, phone: phone, date: date, time: time, guests: guests, table: table, status: 'confirmed' });
    document.getElementById('bk-name').value   = '';
    document.getElementById('bk-phone').value  = '';
    document.getElementById('bk-guests').value = '';
    renderBookings();
    alert('✅ Đặt bàn thành công!');
}

function cancelBooking(idx) {
    if (confirm('Huỷ đặt bàn này?')) { BOOKINGS.splice(idx, 1); renderBookings(); }
}

function renderBookings() {
    var tbody = document.getElementById('booking-tbody');
    if (!BOOKINGS.length) { tbody.innerHTML = '<tr><td colspan="6" class="text-center py-8 text-gray-400 text-xs">Chưa có đặt bàn</td></tr>'; return; }
    tbody.innerHTML = BOOKINGS.map(function(b, i) {
        return '<tr class="border-b border-gray-50 hover:bg-orange-50/30 text-sm">'
            + '<td class="px-4 py-2.5 font-bold">' + b.name + '</td>'
            + '<td class="px-4 py-2.5 text-gray-500 text-xs">' + (b.phone || '—') + '</td>'
            + '<td class="px-4 py-2.5 text-gray-500 text-xs">' + b.date + ' ' + b.time + '</td>'
            + '<td class="px-4 py-2.5"><span class="px-2 py-0.5 bg-blue-100 text-blue-700 rounded-full text-xs font-bold">' + b.table + '</span></td>'
            + '<td class="px-4 py-2.5 text-center">' + b.guests + '</td>'
            + '<td class="px-4 py-2.5"><button onclick="cancelBooking(' + i + ')" class="bg-red-100 text-red-600 hover:bg-red-200 text-xs font-bold px-2.5 py-1 rounded-lg transition">Huỷ</button></td>'
            + '</tr>';
    }).join('');
}

// ── CA LÀM VIỆC ────────────────────────────────────────
function registerShift() {
    var date  = document.getElementById('sh-date').value;
    var shift = document.getElementById('sh-shift').value;
    var role  = document.getElementById('sh-role').value;
    if (!date) { alert('Vui lòng chọn ngày!'); return; }

    SHIFTS.unshift({ date: date, shift: shift, role: role, name: localStorage.getItem('fullName'), status: 'registered' });
    renderShifts();
    alert('✅ Đăng ký ca thành công!');
}

function renderShifts() {
    var tbody = document.getElementById('shift-tbody');
    if (!SHIFTS.length) { tbody.innerHTML = '<tr><td colspan="4" class="text-center py-8 text-gray-400 text-xs">Chưa đăng ký ca nào</td></tr>'; return; }
    tbody.innerHTML = SHIFTS.map(function(s) {
        return '<tr class="border-b border-gray-50 hover:bg-orange-50/30 text-sm">'
            + '<td class="px-4 py-2.5 font-bold">' + s.date + '</td>'
            + '<td class="px-4 py-2.5 text-gray-600 text-xs">' + SHIFT_LABEL[s.shift] + '</td>'
            + '<td class="px-4 py-2.5"><span class="px-2 py-0.5 bg-orange-100 text-orange-700 rounded-full text-xs font-bold">' + s.role + '</span></td>'
            + '<td class="px-4 py-2.5"><span class="px-2 py-0.5 bg-green-100 text-green-700 rounded-full text-xs font-bold">Đã đăng ký</span></td>'
            + '</tr>';
    }).join('');
}

// ── Init ───────────────────────────────────────────────
showTab('invoice');