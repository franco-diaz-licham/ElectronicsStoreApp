
<h1>🛒 Electronics Store App</h1>

<p><strong>Electronics Store App</strong> is a front-end web application that mimics a modern electronics e-commerce site. 
Built with <strong>Vue 3 + TypeScript</strong>, it features a clean component architecture, local JSON data, and a persisted shopping cart.</p>

<blockquote>ℹ️ <em>This project is the front-end only. It reads mock data from <code>/public/data/*.json</code>.</em></blockquote>

<p align="center">
  <img src="/docs/electronicsapp.gif" />
</p>


<hr />

<h2>✨ Features</h2>
<ul>
  <li><strong>Product catalogue</strong> with brand/category filters, sorting, and pagination</li>
  <li><strong>Product details</strong> pages</li>
  <li><strong>Shopping cart</strong> with quantity updates, totals, GST, and <strong>localStorage</strong> persistence</li>
  <li><strong>Account pages</strong> with <strong>VeeValidate + Zod</strong> forms (Login, Signup, Profile)</li>
  <li><strong>Contact us</strong> form with validation, dynamic alerts, and store locations</li>
  <li><strong>Responsive UI</strong> using Bootstrap grid & utilities</li>
  <li><strong>Query caching</strong> and request deduplication via <strong>TanStack Vue Query</strong></li>
  <li><strong>Reusable UI components</strong> (inputs, toasts, banners, filter widgets)</li>
  <li><strong>Accessible UX</strong> patterns (aria-live alerts, proper labels, keyboard-friendly)</li>
</ul>

<hr />

<h2>🧰 Technology Stack</h2>

<h3>Frontend — Vue 3 + TypeScript</h3>
<ul>
  <li><strong>Vue 3</strong> (Composition API)</li>
  <li><strong>Vue Router 4</strong></li>
  <li><strong>Pinia 3</strong> (cart/profile stores with persistence)</li>
  <li><strong>TanStack Vue Query 5</strong> (server cache, query keys, fetch lifecycles)</li>
  <li><strong>VeeValidate 4 + Zod</strong> (schema-first validation)</li>
  <li><strong>Axios</strong> (HTTP client, base URL via env)</li>
  <li><strong>Bootstrap 5.3 + Bootstrap Icons</strong></li>
</ul>

<h3>Tooling</h3>
<ul>
  <li><strong>Vite 7</strong></li>
  <li><strong>TypeScript 5</strong></li>
  <li><strong>vue-tsc</strong> type checking</li>
  <li><strong>ESM modules</strong></li>
</ul>

<hr />

<h2>📁 Project Structure</h2>
<pre>
/ (repo root)
├─ index.html
├─ package.json
├─ public/
│  ├─ images/                # marketing and UI assets
│  └─ data/                  # mock JSON data (brands, categories, products)
└─ src/
   ├─ app/                   # root app (App.vue, main.ts, routes.ts)
   ├─ entities/              # cross-feature domain objects
   ├─ features/              # vertical slices
   │  ├─ products/           # list + details, API & models
   │  ├─ brands/             # brands API & models
   │  ├─ categories/         # categories API & models
   │  ├─ basket/             # basket models
   │  ├─ account/            # login, signup, profile
   │  ├─ shoppingCart/       # cart page
   │  ├─ home/               # homepage sections
   │  ├─ aboutUs/            # values & marketing
   │  └─ contactUs/          # contact form, faq, locations
   ├─ shared/                # design system & cross-cutting concerns
   │  ├─ api/                # axios client, query keys, response types
   │  ├─ assets/             # styles, icons, images
   │  ├─ components/         # TextInput, SelectInput, FilterBox, etc.
   │  ├─ composables/        # useToast, useAlert
   │  ├─ layout/             # AppNavbar, AppFooter, AppToast
   │  └─ lib/                # helpers & mappers
   ├─ stores/                # Pinia stores (cart, profile)
   └─ env.d.ts
</pre>

<hr />

<h2>🚀 Getting Started</h2>
<ol>
  <li><strong>Install dependencies:</strong><br />
  <pre><code>npm install</code></pre></li>

  <li><strong>(Optional) Configure environment:</strong><br />
  <pre><code>VITE_API_URL=/</code></pre></li>

  <li><strong>Run the dev server:</strong><br />
  <pre><code>npm run dev</code></pre></li>

  <li><strong>Build for production:</strong><br />
  <pre><code>npm run build</code></pre></li>

  <li><strong>Preview the production build:</strong><br />
  <pre><code>npm run preview</code></pre></li>

  <li><strong>Type-check only:</strong><br />
  <pre><code>npm run typecheck</code></pre></li>
</ol>

<hr />

<h2>🛒 Cart Persistence</h2>
<p>The cart is stored with <strong>Pinia</strong> and persisted to <strong>localStorage</strong> (<code>stores/cartStore.ts</code>) so users keep their basket across sessions.</p>

<hr />

<h2>🔧 Scripts (package.json)</h2>
<pre><code>{
  "scripts": {
    "dev": "vite",
    "build": "vue-tsc -b && vite build",
    "preview": "vite preview",
    "typecheck": "vue-tsc --noEmit -p tsconfig.json"
  }
}</code></pre>

<hr />

<h2>🧭 Routes</h2>
<ul>
  <li><code>/</code> – Home</li>
  <li><code>/products</code> – Catalogue (with filters)</li>
  <li><code>/products/:id</code> – Product details</li>
  <li><code>/cart</code> – Shopping cart</li>
  <li><code>/about-us</code> – About</li>
  <li><code>/contact-us</code> – Contact & locations</li>
  <li><code>/account</code> – My account</li>
  <li><code>/login</code> – Login</li>
  <li><code>/signup</code> – Signup</li>
</ul>

<hr />

<h2>♿ Accessibility & UX</h2>
<ul>
  <li>Form inputs have labels and validation messages</li>
  <li>Alerts use <code>aria-live="polite"</code> for accessibility</li>
  <li>Keyboard-friendly navigation via Bootstrap focus styles</li>
</ul>

<hr />

<h2>📄 License</h2>
<p>MIT</p>
