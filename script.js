document.addEventListener('DOMContentLoaded', function() {
    const newPlanBtn = document.getElementById('newPlanBtn');
    const newPlanIdea = document.getElementById('newPlanIdea');
    
    // Kosár inicializálása
    if (typeof Cart !== 'undefined') {
        window.cart = new Cart();
    }
    
    // Kosár gombok eseménykezelője
    document.addEventListener('click', function(e) {
        if (e.target.closest('.add-to-cart')) {
            const button = e.target.closest('.add-to-cart');
            const card = button.closest('.card');
            const product = {
                id: generateProductId(card),
                name: card.querySelector('.card-title').textContent,
                price: card.querySelector('.text-primary').textContent,
                category: card.querySelector('.badge').textContent,
                image: card.querySelector('img').src
            };
            
            if (window.cart) {
                window.cart.addItem(product);
            } else {
                showNotification(`${product.name} hozzáadva a kosárhoz!`);
            }
        }
    });
    
    function generateProductId(card) {
        const title = card.querySelector('.card-title').textContent;
        return 'product_' + title.replace(/\s+/g, '_').toLowerCase();
    }
    
    function showNotification(message) {
        const notification = document.createElement('div');
        notification.className = 'alert alert-success position-fixed';
        notification.style.top = '20px';
        notification.style.right = '20px';
        notification.style.zIndex = '9999';
        notification.style.minWidth = '300px';
        notification.textContent = message;
        
        document.body.appendChild(notification);
        
        setTimeout(() => {
            notification.remove();
        }, 3000);
    }
    
    newPlanBtn.addEventListener('click', function() {
        const plans = [
            "Próbáld ki a 'Focista robbanékonyság' programot, amely speciális ugró- és sprintgyakorlatokkal segíti a meccshelyzetekben való gyors reagálást!",
            "A 'HIIT és erőnlét kombináció' ideális választás azoknak, akik hatékonyan szeretnék fejleszteni mind az állóképességüket, mind az izomerejüket!",
            "Tekintsd meg a 'Teljes test funkcionális erő' programot, amely mindennapi mozgásokra épülve fejleszti az erőt, egyensúlyt és koordinációt!",
            "A 'Futás technika és sebesség' program kiváló választás azoknak, akik szeretnék javítani futásformájukat és növelni a tempójukat!",
            "Próbáld ki az 'Otthoni yoga flow' programot, amely rugalmasságot, erőt és mentális nyugalmat hoz az életedbe, akár otthoni környezetben is!"
        ];
        
        const randomPlan = plans[Math.floor(Math.random() * plans.length)];
        
        newPlanIdea.textContent = randomPlan;
        newPlanIdea.classList.remove('d-none');
        
        setTimeout(() => {
            newPlanIdea.classList.add('d-none');
        }, 8000);
    });
    
    const carousel = document.getElementById('trainingCarousel');
    if (carousel) {
        carousel.addEventListener('slide.bs.carousel', function (e) {
            const activeItem = this.querySelector('.carousel-item.active');
            activeItem.classList.remove('active');
        });
        
        carousel.addEventListener('slid.bs.carousel', function (e) {
            const newActiveItem = this.querySelector('.carousel-item:nth-child(' + (e.to + 1) + ')');
            newActiveItem.classList.add('active');
        });
    }
    
    console.log(`
        Üdvözöljük az RMB Coaching weboldalán!
        
        Fejlesztők:
        - Bakaja Csaba (Foci szakértő)
        - Rubovszki Balázs (Kondi edző)
        - Amesei Botond (Általános fitness edző)
        
        Köszönjük, hogy meglátogattad oldalunkat!
    `);
});