<template>
    <b-row class="justify-content-md-center mt-3 mb-3">
        <b-col class="col-lg-3 text-center">
            <b-button variant="outline-primary" size="lg" @click="signInWithGoogle">
                <b-icon icon="google"/> Log in with Google
            </b-button>
        </b-col>
    </b-row>
</template>

<script>
import api from '@/services/PharmacyApiService'

export default {
    name: 'GoogleButton',
    methods: {
        getDateOfBirthFromGoogle(birthdays) {
            for (const birthday in birthdays) {
                const date = birthdays[birthday].date;
                if (typeof date.year !== 'undefined' && 
                    typeof date.month !== 'undefined' && 
                    typeof date.day !== 'undefined') {
                        return new Date(date.year, date.month - 1, date.day + 1);
                }
            }
            return undefined;
        },
        signInWithGoogle() {
            this.$store.dispatch('user/signInWithGoogle').then((user) => {
                if (user.additionalUserInfo.isNewUser) {
                    this.$store.dispatch('user/getGoogleApiData', {
                        id: user.additionalUserInfo.profile.id, 
                        accessToken: user.credential.accessToken
                    }).then(async (result) => {
                        const birthday = this.getFormattedDate(this.getDateOfBirthFromGoogle(result.birthdays));
                        await api.createAccount({
                            name: user.additionalUserInfo.profile.name,
                            email: user.additionalUserInfo.profile.email,
                            phone: result.phoneNumbers === undefined ? undefined : result.phoneNumbers[0].value,
                            dateOfBirth: birthday === undefined ? undefined : birthday,
                            gender: result.genders === undefined ? undefined : result.genders[0].value
                        });
                    }).catch((error) => {
                        
                    }).finally(() => {
                        this.onSignedIn();
                    });
                    return;
                }
                this.onSignedIn();
            }).catch((error) => {
                const message = error.message.split('.');
                this.makeToast(message[0], message[1] + '.', "danger");
            });
        }
    },
    props: {
        onSignedIn: {
            type: Function,
            required: true
        }
    }
}
</script>

<style scoped>

</style>