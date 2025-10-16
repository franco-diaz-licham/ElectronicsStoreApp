<template>
    <div id="contain" class="d-flex justify-content-center align-items-center text-center flex-column">
        <h1>
            Sorry, we couldn't find what <br />
            you were looking for...
        </h1>
        <RouterLink class="text-decoration-underline fw-bold" to="/">
            <button class="btn btn-primary mt-3"><i class="bi bi-house-door-fill fs-5 me-2"></i>Go Home</button>
        </RouterLink>
    </div>
</template>
<style scoped>
#contain {
    min-height: calc(100vh - 226px);
}

.login-container {
    width: 100%;
    max-width: 420px;
    border: 1px solid #dee2e6;
    border-radius: 1rem;
    padding: 2rem;
    background: #fff;
}

.logo-placeholder {
    width: 100px;
    height: 100px;
    background-color: #f1f3f5;
    border: 1px solid #dee2e6;
    margin: 0 auto 1rem;
    border-radius: 12%;
}
</style>
