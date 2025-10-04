<template>
    <div class="col-md-6">
        <h4 class="mb-3">FAQs</h4>
        <div class="accordion shadow" id="faq">
            <div class="accordion-item" v-for="(faq, i) in models" :key="i">
                <h2 class="accordion-header" :id="`q${i}h`">
                    <button class="accordion-button" :class="{ collapsed: i !== 0 }" type="button" :data-bs-toggle="'collapse'" :data-bs-target="`#q${i}`" :aria-expanded="i === 0 ? 'true' : 'false'" :aria-controls="`q${i}`">
                        {{ i + 1 }}. {{ faq.question }}
                    </button>
                </h2>
                <div :id="`q${i}`" class="accordion-collapse collapse" :class="{ show: i === 0 }" :aria-labelledby="`q${i}h`" data-bs-parent="#faq">
                    <div class="accordion-body">{{ faq.answer }}</div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import type { PropType } from "vue";
import type { faqModel } from "../models/contact.type";

defineProps({
    models: { type: Array as PropType<faqModel[]>, required: true },
});
</script>
