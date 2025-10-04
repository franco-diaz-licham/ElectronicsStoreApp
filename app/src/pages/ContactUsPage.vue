<template>
    <!-- Banner -->
    <HeroBanner title="Contact Us" subtitle="Get in touch" />

    <!-- Store Locations -->
    <section class="my-5">
        <div class="row text-center g-4">
            <div v-for="s in stores" :key="s.name" class="col-md-4">
                <Location :address="s.address" :name="s.name" :phone="s.phone" :image="s.image" />
            </div>
        </div>
    </section>
    <hr class="w-100" />

    <!-- FAQ + Form -->
    <section class="my-5">
        <div class="row g-4">
            <!-- FAQ -->
            <Faq :models="faqs" />

            <!-- Contact Form -->
            <div class="col-md-6">
                <h4 class="mb-3">Contact Form</h4>
                <ContactForm @form-submit="handleSubmit" />
            </div>
        </div>
    </section>
</template>

<script setup lang="ts">
import type { ContactFormModel, faqModel, storeModel } from "../features/contactUs/models/contact.type";
import HeroBanner from "../shared/components/HeroBanner.vue";
import Location from "../features/contactUs/components/Location.vue";
import ContactForm from "../features/contactUs/components/ContactForm.vue";
import Faq from "../features/contactUs/components/FAQ.vue";
import { onMounted, ref } from "vue";

const stores = ref<storeModel[]>([]);
const faqs = ref<faqModel[]>([]);

onMounted(async () => {
    getData();
});

/** Loads sample data. */
const getData = () => {
    stores.value = [
        { name: "Store 1", address: "123 Example St, Sydney NSW 2000", phone: "(02) 1234 5678", image: "/images/location-1.png" },
        { name: "Store 2", address: "45 Sample Ave, Melbourne VIC 3000", phone: "(03) 1234 5678", image: "/images/location-2.png" },
        { name: "Store 3", address: "9 Demo Rd, Brisbane QLD 4000", phone: "(07) 1234 5678", image: "/images/location-3.png" },
    ];

    faqs.value = [
        {
            question: "Lorem ipsum dolor sit amet?",
            answer: "Lorem ipsum dolor sit amet consectetur, adipisicing elit. Nam, debitis voluptas! Dolore sunt voluptates eaque, natus esse voluptate ad similique maxime vero totam quam reiciendis fuga iusto voluptatum officia? Tenetur non vel reprehenderit eaque quisquam porro nemo, temporibus consequatur quaerat libero quas sequi impedit ex harum ipsa, id assumenda culpa!",
        },
        {
            question: "Ut enim ad minim veniam?",
            answer: "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Iste accusantium perferendis pariatur obcaecati cum quas totam voluptatum nihil dolorem enim. Ad, doloremque dolores itaque quia ex ipsam quidem eum nesciunt hic quas repudiandae tempore accusantium dolore totam nam doloribus quis dolorum obcaecati beatae corporis et possimus labore nulla? Ab provident blanditiis odit enim eius veritatis autem magnam sapiente perferendis! Quaerat laudantium praesentium consequuntur dolore commodi distinctio voluptate nemo, inventore ipsa delectus ratione vitae reiciendis. Veniam reprehenderit eligendi atque maiores eos. A ratione eligendi dolores vero dicta facilis aspernatur aperiam architecto molestiae omnis. Voluptatem dolore maiores accusamus minus excepturi provident corrupti.",
        },
        {
            question: "Sed do eiusmod tempor incididunt?",
            answer: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nisi quos voluptatibus sed at impedit eum quas similique accusantium a ullam nostrum rem provident aperiam ad, cupiditate perspiciatis voluptates, praesentium rerum fuga aliquam harum inventore. Assumenda vitae rerum, suscipit expedita est voluptatibus fugiat ut, soluta iusto deleniti veritatis. Unde consequuntur perspiciatis doloremque. Dolor quos voluptatum ipsa est, sit quas sapiente quis possimus repellat aliquid consequuntur! Delectus aut ab doloremque dolorum accusantium!.",
        },
        {
            question: "Consectetur adipiscing elit?",
            answer: "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Rerum dolor inventore qui libero, aut architecto eligendi magni amet molestiae veritatis exercitationem nostrum adipisci, praesentium, quidem illo voluptatem quas accusantium nisi voluptatum illum minima labore ipsa. Quidem incidunt ea perferendis necessitatibus.",
        },
    ];
};

/** Handles form submission. */
function handleSubmit(data: ContactFormModel) {
    console.log(data);
}
</script>
