document.addEventListener('DOMContentLoaded', function() {
    const form = document.getElementById('assessmentForm');
    const result = document.getElementById('result');
    const planDetails = document.getElementById('planDetails');
    
    // Kosár inicializálása
    if (typeof Cart !== 'undefined') {
        window.cart = new Cart();
    }
    
    form.addEventListener('submit', function(e) {
        e.preventDefault();
        
        const weight = parseInt(document.getElementById('weight').value);
        const height = parseInt(document.getElementById('height').value);
        const age = parseInt(document.getElementById('age').value);
        const gender = document.querySelector('input[name="gender"]:checked').value;
        const experience = document.getElementById('experience').value;
        const frequency = document.getElementById('frequency').value;
        const duration = document.getElementById('duration').value;
        const sport = document.getElementById('sport').value;
        const equipment = document.getElementById('equipment').value;
        
        const goals = [];
        const goalCheckboxes = document.querySelectorAll('input[type="checkbox"]:checked');
        goalCheckboxes.forEach(checkbox => {
            goals.push(checkbox.value);
        });
        
        const bmi = calculateBMI(weight, height);
        const plan = generateTrainingPlan(bmi, age, gender, experience, frequency, duration, goals, sport, equipment);
        
        displayPlan(plan);
        
        form.classList.add('d-none');
        result.classList.remove('d-none');
        
        window.scrollTo({
            top: result.offsetTop - 100,
            behavior: 'smooth'
        });
    });
    
    function calculateBMI(weight, height) {
        const heightInMeters = height / 100;
        return weight / (heightInMeters * heightInMeters);
    }
    
    function generateTrainingPlan(bmi, age, gender, experience, frequency, duration, goals, sport, equipment) {
        let plan = {
            name: '',
            description: '',
            duration: '',
            frequency: '',
            focus: [],
            recommendations: [],
            suggestedProducts: []
        };
        
        if (sport === 'football') {
            plan.name = 'Labdarúgás fókuszú edzésterv';
            
            if (experience === 'beginner') {
                plan.duration = '8 hét';
                plan.frequency = '3 alkalom/hét';
                plan.description = 'Ez a kezdő labdarúgó edzésterv az alapokra fókuszál, beleértve a labdaérintést, futási technikát és állóképességet.';
                plan.focus = ['Alap labdaérintés gyakorlatok', 'Futási technika fejlesztése', 'Állóképesség építés'];
                plan.suggestedProducts = [
                    { 
                        id: 'product_focista_allokepesseg',
                        name: 'Focista állóképesség', 
                        price: '12.990 Ft', 
                        category: 'Labdarúgás',
                        image: 'https://images.unsplash.com/photo-1575361204480-aadea25e6e68?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    },
                    { 
                        id: 'product_labdaerintes_es_technika',
                        name: 'Labdaérintés és technika', 
                        price: '9.990 Ft', 
                        category: 'Labdarúgás',
                        image: 'https://images.unsplash.com/photo-1529900748604-07564a03e7a6?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    }
                ];
            } else if (experience === 'intermediate') {
                plan.duration = '10 hét';
                plan.frequency = '4 alkalom/hét';
                plan.description = 'Középhaladó focista edzésterv, amely a technikai készségek tökéletesítésére és a fizikai teljesítmény növelésére összpontosít.';
                plan.focus = ['Technikai gyakorlatok komplexitása', 'Sebesség és gyorsaság fejlesztése', 'Erőnléti edzés'];
                plan.suggestedProducts = [
                    { 
                        id: 'product_eronlet_fejlesztes',
                        name: 'Erőnlét fejlesztés', 
                        price: '11.490 Ft', 
                        category: 'Labdarúgás',
                        image: 'https://images.unsplash.com/photo-1599058917212-d750089bc07e?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    },
                    { 
                        id: 'product_focista_allokepesseg',
                        name: 'Focista állóképesség', 
                        price: '12.990 Ft', 
                        category: 'Labdarúgás',
                        image: 'https://images.unsplash.com/photo-1575361204480-aadea25e6e68?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    }
                ];
            } else {
                plan.duration = '12 hét';
                plan.frequency = '5 alkalom/hét';
                plan.description = 'Haladó szintű labdarúgó program, amely a teljes fizikai és technikai teljesítmény maximalizálását célozza.';
                plan.focus = ['Verseny-specifikus edzések', 'Maximális teljesítmény fejlesztése', 'Stratégiai elemzés'];
                plan.suggestedProducts = [
                    { 
                        id: 'product_focista_allokepesseg',
                        name: 'Focista állóképesség', 
                        price: '12.990 Ft', 
                        category: 'Labdarúgás',
                        image: 'https://images.unsplash.com/photo-1575361204480-aadea25e6e68?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    },
                    { 
                        id: 'product_eronlet_fejlesztes',
                        name: 'Erőnlét fejlesztés', 
                        price: '11.490 Ft', 
                        category: 'Labdarúgás',
                        image: 'https://images.unsplash.com/photo-1599058917212-d750089bc07e?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    }
                ];
            }
            
            if (goals.includes('endurance')) {
                plan.focus.push('Futási állóképesség növelése');
            }
            
            if (goals.includes('strength')) {
                plan.focus.push('Alsótest erősítés');
            }
        } else if (sport === 'fitness') {
            plan.name = 'Kondicionáló edzésterv';
            
            if (experience === 'beginner') {
                plan.duration = '8 hét';
                plan.frequency = '3 alkalom/hét';
                plan.description = 'Alap kondicionáló program kezdőknek, amely biztonságosan bevezet a súlyzós edzés világába.';
                plan.focus = ['Teljes test edzés', 'Alapgyakorlatok elsajátítása', 'Testtudatosság fejlesztése'];
                plan.suggestedProducts = [
                    { 
                        id: 'product_teljes_test_erosites',
                        name: 'Teljes test erősítés', 
                        price: '8.990 Ft', 
                        category: 'Kondicionálás',
                        image: 'https://images.unsplash.com/photo-1534367507877-0edd93bd013b?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    },
                    { 
                        id: 'product_hiit_zsiregetes',
                        name: 'HIIT zsírégetés', 
                        price: '7.990 Ft', 
                        category: 'Kondicionálás',
                        image: 'https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    }
                ];
            } else if (experience === 'intermediate') {
                plan.duration = '10 hét';
                plan.frequency = '4 alkalom/hét';
                plan.description = 'Középhaladó kondicionáló program, amely az izomtömeg növelésére és az erőnlét javítására fókuszál.';
                plan.focus = ['Testrészekre bontott edzés', 'Progresszív terhelés', 'Cardio integráció'];
                plan.suggestedProducts = [
                    { 
                        id: 'product_eroemelo_alapok',
                        name: 'Erőemelő alapok', 
                        price: '10.990 Ft', 
                        category: 'Kondicionálás',
                        image: 'https://images.unsplash.com/photo-1549060279-7e168fce7090?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    },
                    { 
                        id: 'product_teljes_test_erosites',
                        name: 'Teljes test erősítés', 
                        price: '8.990 Ft', 
                        category: 'Kondicionálás',
                        image: 'https://images.unsplash.com/photo-1534367507877-0edd93bd013b?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    }
                ];
            } else {
                plan.duration = '12 hét';
                plan.frequency = '5 alkalom/hét';
                plan.description = 'Haladó kondicionáló program a maximális erő- és izomnövekedés eléréséhez.';
                plan.focus = ['Speciális edzés technikák', 'Csúcsformára edzés', 'Periodizáció'];
                plan.suggestedProducts = [
                    { 
                        id: 'product_eroemelo_alapok',
                        name: 'Erőemelő alapok', 
                        price: '10.990 Ft', 
                        category: 'Kondicionálás',
                        image: 'https://images.unsplash.com/photo-1549060279-7e168fce7090?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    },
                    { 
                        id: 'product_hiit_zsiregetes',
                        name: 'HIIT zsírégetés', 
                        price: '7.990 Ft', 
                        category: 'Kondicionálás',
                        image: 'https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?ixlib=rb-4.0.3&auto=format&fit=crop&w=1350&q=80'
                    }
                ];
            }
            
            if (goals.includes('weightloss')) {
                plan.focus.push('Zsírégető cardio edzések');
                plan.recommendations.push('Napi 30-45 perc közepes intenzitású cardio a gyorsabb eredményekért');
            }
            
            if (goals.includes('muscle')) {
                plan.focus.push('Nehéz összetett gyakorlatok');
                plan.recommendations.push('Növeld a fehérjebevitelt az izomnövekedés támogatására');
            }
        } else {
            // Egyéb sportágak...
        }
        
        return plan;
    }
    
    function displayPlan(plan) {
        let html = `
            <h4 class="text-success mb-3">${plan.name}</h4>
            <p class="mb-3">${plan.description}</p>
            
            <div class="row mb-4">
                <div class="col-md-6">
                    <h5>Program részletei</h5>
                    <ul class="list-group list-group-flush">
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Időtartam
                            <span class="badge bg-primary rounded-pill">${plan.duration}</span>
                        </li>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            Heti gyakoriság
                            <span class="badge bg-primary rounded-pill">${plan.frequency}</span>
                        </li>
                    </ul>
                </div>
            </div>
            
            <h5>Fókuszterületek</h5>
            <ul class="list-group mb-4">
        `;
        
        plan.focus.forEach(focus => {
            html += `<li class="list-group-item">${focus}</li>`;
        });
        
        html += `</ul>`;
        
        if (plan.suggestedProducts && plan.suggestedProducts.length > 0) {
            html += `
                <h5 class="mt-5">Ajánlott edzéstervek</h5>
                <div class="row">
            `;
            
            plan.suggestedProducts.forEach(product => {
                html += `
                    <div class="col-md-6 mb-3">
                        <div class="card h-100">
                            <div class="card-body d-flex flex-column">
                                <h6 class="card-title">${product.name}</h6>
                                <div class="d-flex justify-content-between align-items-center mt-auto">
                                    <span class="badge bg-primary">${product.category}</span>
                                    <span class="fw-bold text-primary">${product.price}</span>
                                </div>
                                <button class="btn btn-primary btn-sm w-100 mt-2 add-suggested-product" 
                                        data-product='${JSON.stringify(product)}'>
                                    <i class="fas fa-cart-plus me-1"></i>Kosárba
                                </button>
                            </div>
                        </div>
                    </div>
                `;
            });
            
            html += `</div>`;
        }
        
        html += `</div>`;
        
        planDetails.innerHTML = html;
        
        // Ajánlott termékek kosárba helyezése
        document.querySelectorAll('.add-suggested-product').forEach(button => {
            button.addEventListener('click', function() {
                const product = JSON.parse(this.dataset.product);
                if (window.cart) {
                    window.cart.addItem(product);
                } else {
                    alert('Termék hozzáadva a kosárhoz!');
                }
            });
        });
    }
});