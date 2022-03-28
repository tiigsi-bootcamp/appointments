<script setup lang="ts">
import { TokenService } from '../services/token-service';
import { useRouter } from 'vue-router';

const router = useRouter();

TokenService.delete();
router.push('/');
</script>
