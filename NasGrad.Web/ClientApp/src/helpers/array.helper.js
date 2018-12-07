
export const arrayHelper = () => {
    Array.prototype.skip = function (count) {
        return this.filter((_, i) => i >= count);
    };

    Array.prototype.take = function (count) {
        return this.filter((_, i) => i < count);
    };
}