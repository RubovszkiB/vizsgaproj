// Új edzésterv ötlet generálása
document.getElementById('newPlanBtn').addEventListener('click', function() {
    const trainingIdeas = [
        "Hogyan találd meg a motivációt az edzéshez? - 5 hetes mentális felkészítő program",
        "Fókusz a középtájon: 6 hetes hasizom-erősítő program kezdőknek",
        "Reggeli aktiválás: 15 perces otthoni gyakorlatsor minden napra",
        "Feszültségoldás munka után: nyújtós és légzőgyakorlatok sorozata",
        "Erő és állóképesség kombinációja: 8 hetes crossfit alapok program",
        "Futás technika fejlesztése: gyakorlatok a helyes futáshoz",
        "Teljes test erősítés saját testsúllyal: 30 napos kihívás",
        "Mobilitás javítás: 4 hetes program az ízületek és izmok mozgékonyságáért",
        "Sportolói regeneráció: 3 hetes program a gyorsabb felépülésért",
        "Fókusz a hátizomra: 6 hetes program erős és esztétikus hátizomért",
        "Láb nap fejlesztése: 8 hetes program erősebb és formásabb lábakért",
        "Kardió intervallumok: 4 hetes program a maximális zsírégetésért",
        "Feszesség és rugalmasság: 6 hetes nyújtóprogram minden sportolónak",
        "Erős kézfej: 5 hetes program a markolóerő és alkarizom fejlesztésére",
        "Sportolói táplálkozás: 4 hetes étrendi tanácsadó program"
    ];
    
    const randomIdea = trainingIdeas[Math.floor(Math.random() * trainingIdeas.length)];
    const ideaElement = document.getElementById('newPlanIdea');
    
    ideaElement.innerHTML = `<strong>Új terv ötlet:</strong> ${randomIdea}`;
    ideaElement.classList.remove('d-none');
    
    // Animáció hozzáadása
    ideaElement.classList.add('fade-in');
    
    // Gomb szövegének átalakítása
    this.innerHTML = '<i class="fas fa-sync-alt me-2"></i>Új ötlet';
    
    // 3 másodperc után visszaállítjuk az eredeti szöveget
    setTimeout(() => {
        this.innerHTML = '<i class="fas fa-lightbulb me-2"></i>Új edzésterv ötlet';
    }, 3000);
});

// Kosár funkció
let cartCount = 0;
const cartCountElement = document.querySelector('.cart-count');

document.querySelectorAll('.add-to-cart').forEach(button => {
    button.addEventListener('click', function() {
        const cardTitle = this.closest('.card').querySelector('.card-title').textContent;
        const cardPrice = this.closest('.card').querySelector('.text-primary').textContent;
        
        // Kosár számláló növelése
        cartCount++;
        cartCountElement.textContent = cartCount;
        
        // Animáció a kosár ikonon
        cartCountElement.parentElement.classList.add('pulse');
        setTimeout(() => {
            cartCountElement.parentElement.classList.remove('pulse');
        }, 500);
        
        // Értesítés megjelenítése
        showNotification(`${cardTitle} hozzáadva a kosárhoz! - ${cardPrice}`);
    });
});

// Értesítés funkció
function showNotification(message) {
    // Értesítés elem létrehozása
    const notification = document.createElement('div');
    notification.className = 'alert alert-success position-fixed';
    notification.style.top = '20px';
    notification.style.right = '20px';
    notification.style.zIndex = '1050';
    notification.style.minWidth = '300px';
    notification.textContent = message;
    
    // Hozzáadás a dokumentumhoz
    document.body.appendChild(notification);
    
    // Automatikus eltávolítás 3 másodperc után
    setTimeout(() => {
        notification.remove();
    }, 3000);
}

// Navbar átalakulása scrollozáskor
window.addEventListener('scroll', function() {
    const navbar = document.querySelector('.navbar');
    if (window.scrollY > 100) {
        navbar.classList.add('navbar-scrolled');
    } else {
        navbar.classList.remove('navbar-scrolled');
    }
});

// Sima görgetés anchor linkekre
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        
        const targetId = this.getAttribute('href');
        if (targetId === '#') return;
        
        const targetElement = document.querySelector(targetId);
        if (targetElement) {
            window.scrollTo({
                top: targetElement.offsetTop - 80,
                behavior: 'smooth'
            });
        }
    });
});

// CSS osztály hozzáadása a pulzáló animációhoz
const style = document.createElement('style');
style.textContent = `
    .pulse {
        animation: pulse 0.5s ease-in-out;
    }
    
    @keyframes pulse {
        0% { transform: scale(1); }
        50% { transform: scale(1.2); }
        100% { transform: scale(1); }
    }
    
    .navbar-scrolled {
        background-color: rgba(255, 255, 255, 0.95) !important;
        backdrop-filter: blur(10px);
        box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        padding: 0.5rem 0 !important;
    }
`;
document.head.appendChild(style);

// Oldal betöltésének animációja
document.addEventListener('DOMContentLoaded', function() {
    // Hero szekció animációja
    const heroTitle = document.querySelector('.hero-title');
    const heroSubtitle = document.querySelector('.hero-subtitle');
    const heroCta = document.querySelector('.hero-cta');
    
    setTimeout(() => {
        heroTitle.classList.add('fade-in');
    }, 300);
    
    setTimeout(() => {
        heroSubtitle.classList.add('fade-in');
    }, 600);
    
    setTimeout(() => {
        heroCta.classList.add('fade-in');
    }, 900);
    
    // Karuszel elemek animációja
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('fade-in');
            }
        });
    }, observerOptions);
    
    document.querySelectorAll('.training-card, .trainer-card').forEach(card => {
        observer.observe(card);
    });
});

// Konzol üdvözlés
console.log(`
    Üdvözöljük az RMB Coaching weboldalán!
    
    Fejlesztők:
    - Bakaja Csaba (Foci szakértő)
    - Rubovszki Balázs (Kondi edző)
    - Amesei Botond (Általános fitness edző)
    
    Köszönjük, hogy meglátogattad oldalunkat!
`);