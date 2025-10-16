<template>
    <!-- Hero -->
    <HeroBanner title="About Us" subtitle="Our Story" />

    <!-- Mission -->
    <section class="my-5">
        <h2 class="text-center mb-3 h3">Our Mission</h2>
        <p class="text-center mx-auto" style="max-width: 820px">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam.</p>
    </section>

    <hr class="w-100" />

    <!-- History -->
    <section class="mt-5">
        <h2 class="text-center mb-4 h3">Our History</h2>
        <div class="row g-4 align-items-center">
            <div class="col-md-8">
                <h3 class="h5">Early Days</h3>
                <p>
                    Lorem ipsum dolor sit amet consectetur adipisicing elit. Delectus ab adipisci tenetur consectetur quam eaque suscipit vel odio inventore, commodi repellat laborum voluptate provident blanditiis, atque quod nostrum totam unde, aspernatur sed
                    sint placeat. Nihil, sed quasi. Rem, a fugit!
                </p>
                <h3 class="h5">Current</h3>
                <p>
                    Lorem ipsum dolor sit amet consectetur adipisicing elit. Facilis reprehenderit voluptatem laudantium magni, laborum ad neque, at fuga asperiores provident delectus itaque totam corporis, impedit ducimus. Consequuntur ratione molestiae hic
                    optio fugiat explicabo dolorem, officiis odit quos fuga laboriosam voluptate impedit.
                </p>
            </div>
            <div class="col-md-4">
                <figure class="ratio ratio-4x3 rounded shadow overflow-hidden">
                    <img src="/images/store.jpg" alt="Company timeline" class="w-100 h-100" style="object-fit: cover" />
                </figure>
            </div>
        </div>
    </section>

    <!-- Values -->
    <section class="mb-5">
        <h2 class="text-center mb-4 h3">Our Values</h2>
        <ValuesGrid :values="values" />
    </section>
</template>

<script setup lang="ts">
import ValuesGrid from "../features/aboutUs/components/ValuesGrid.vue";
import type { ValueCardModel } from "../features/aboutUs/models/aboutUs.type";
import HeroBanner from "../shared/components/HeroBanner.vue";

const values: ValueCardModel[] = [
    { icon: "bi-emoji-smile", title: "Customer First", text: "We obsess over customer delight." },
    { icon: "bi-lightning", title: "Speed & Quality", text: "Fast, reliable, and secure experiences." },
    { icon: "bi-shield-check", title: "Trust", text: "Transparent policies and fair returns." },
    { icon: "bi-globe2", title: "Sustainability", text: "We aim to reduce waste and footprint." },
];
</script>
